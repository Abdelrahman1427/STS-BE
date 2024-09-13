using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Entities;
using PathFinder.Core.Interface.IService;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.Core.Interfaces.Shared.Repository;
using PathFinder.Core.Interface.Shared.IServices;
using PathFinder.DataTransferObjects.DTOs.Account;
using PathFinder.DataTransferObjects.DTOs.Identity.Response;
using PathFinder.DataTransferObjects.DTOs.Identity.Request;
using PathFinder.DataTransferObjects.Resources;
using PathFinder.SharedKernel.Constants;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.DTOs.Employee;
using PathFinder.SharedKernel.Enums;
using PathFinder.SharedKernel.Extensions;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class AccountService : CrudWithPaginateService<User>, IAccountService
    {
        private readonly JWTOptions _jWTOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _context;
        private readonly IRoleClaimService _roleClaimService;
        private readonly IEncryptEnginService _encryptEnginService;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, IUserService userService,
            IRoleService roleService, IRoleClaimService roleClaimService, 
            IOptions<JWTOptions> jWTOptions, 
            IHttpContextAccessor context,  
            IEmailService emailService,  
            IEncryptEnginService encryptEnginService
            ) : base(unitOfWork)
        {
            _jWTOptions = jWTOptions.Value;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
            _roleClaimService = roleClaimService;
            _context = context;
            _emailService = emailService;
            _encryptEnginService = encryptEnginService;
        }

        public async Task<APIResult> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                LoginResponseDTO loginDTO = new LoginResponseDTO();
                var user = await _userService.GetUserByNameOrEmailOrMobile(loginRequestDTO.UserEmailOrMobile);
                if (user == null)
                    return new APIResult { state = false, message = ModelValidationResources.UserEmailOrMobileValid };

                string decriptedPassword = _encryptEnginService.Decrypt(user.Password?.Trim(), AppConstants.PasswordKey, true).Substring(1).Split('♪')[1];
                if (decriptedPassword != loginRequestDTO.Password)
                    return new APIResult { message = ModelValidationResources.UserEmailOrMobileValid };

                var claims = await CreateClaims(user);
                var claimList = await GetPermissionsAsync(claims.Role, Platform.Website);
                claimList.AddRange(claims.Claims);
                Claim isMobileLogin = new Claim(ClaimConstants.IsMobileLogin, "false");
                claimList.Add(isMobileLogin);

                var jwt = await CreateJwtTokenAsync(claimList);
                var refreshToken = GenerateRefreshToken();

                loginDTO.Claims = claimList.ToDictionary(a => a.Type, a => a.Value);
                loginDTO.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
                loginDTO.RefreshTokenExpiration = refreshToken.ExpiredOn;
                loginDTO.RefreshTokenKey = refreshToken.Token;

                var userTokenRepository = _unitOfWork.GetRepositoryAsync<AccessToken>();
                await userTokenRepository.AddAsync(new AccessToken()
                {
                    UserId = user.Id,
                    LoginProvider = AppConstants.Login,
                    Name = AppConstants.Token,
                    Value = loginDTO.Token,
                    OccurringDate = DateTime.UtcNow,
                });

                var refreshTokenRepository = _unitOfWork.GetRepositoryAsync<RefreshToken>();
                await refreshTokenRepository.AddAsync(new RefreshToken()
                {
                    UserId = user.Id,
                    Token = loginDTO.RefreshTokenKey,
                    ExpiredOn = loginDTO.RefreshTokenExpiration,
                    RevokedOn = DateTimeOffset.UtcNow,
                    CreatedOn = DateTimeOffset.UtcNow,
                });

                await _unitOfWork.SaveChangesAsync();
                return new APIResult { state = true , entity = loginDTO };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        public async Task<APIResult> DeleteAsync(int Id)
        {
            try
            {
                var entity = _unitOfWork.GetRepositoryAsync<User>();
                entity.Delete(Id);

                var entityUser = _unitOfWork.GetRepositoryAsync<User>();
                var user = await entityUser.FirstOrDefaultAsync(a => a.Id == Id);
                entityUser.Delete(Id);

                await _unitOfWork.SaveChangesAsync();
                return new APIResult { state = true };
            }
            catch (DbUpdateException ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }
        public async Task<APIResult> GetById(int id)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var userRepository = _unitOfWork.GetRepositoryAsync<User>();
                var user = await userRepository.FirstOrDefaultAsync(a => a.Id == id);
                if (user == null)
                    throw new ApplicationException(ModelValidationResources.UserNotFound);

                return new APIResult { state = true, entity = _mapper.Map<ViewEmployeeDTO>(user) };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        public async Task<APIResult> GetUser()
        {
            APIResult apiResult = new APIResult();
            try
            {
                apiResult.entity = await _userService.GetUserById(_context.GetUserId());
                if (apiResult.state == false)
                    return apiResult;

                return apiResult;
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }
        public async Task<APIResult> ForgetPassword(SendForgotPasswordDTO sendForgotPasswordDTO)
        {
            try
            {
                var user = await _userService.GetUserByEmail(sendForgotPasswordDTO.Email);
                if (user == null)
                    throw new ApplicationException(ModelValidationResources.WrongEmail);

                //delete code if exisit before
                await DeleteCode(user);
                Random generator = new Random();
                string code = generator.Next(1, 10000).ToString("D4");

                var userTokenRepository = _unitOfWork.GetRepositoryAsync<AccessToken>();
                await userTokenRepository.AddAsync(new AccessToken()
                {
                    UserId = user.Id,
                    LoginProvider = AppConstants.ForgetPassword,
                    Name = AppConstants.Code,
                    Value = code
                });
                await _unitOfWork.SaveChangesAsync();

                APIResult aPIResult = await SendingMail(user, code);
                ForgotPasswordDTO passwordDTO = new ForgotPasswordDTO()
                {
                    Code = code,
                    Email = user.Email
                };
                return new APIResult { state = true, entity = passwordDTO, message = aPIResult.message };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        public async Task<APIResult> ResetPassword(ResetPasswordDTO resetPasswordDto)
        {
            try
            {
                var user = await _userService.GetUserByEmail(resetPasswordDto.Email);
                if (user == null)
                    throw new ApplicationException(ModelValidationResources.WrongEmail);

                var userTokenRepository = _unitOfWork.GetRepositoryAsync<User>();
                user.Password = _encryptEnginService.Encrypt(resetPasswordDto.Password.Trim().ToLower() + "♪" + resetPasswordDto.Password.Trim(), AppConstants.PasswordKey, true);
                userTokenRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();

                return new APIResult { state = true };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        public async Task<APIResult> RefreshTokenAsync(string token)
        {
            try
            {
                var refreshToken = await _unitOfWork.GetRepositoryAsync<RefreshToken>().FirstOrDefaultAsync(a => a.Token == token);
                if (refreshToken == null)
                    throw new ApplicationException(ExceptionConstants.InValidToken);

                var user = await _unitOfWork.GetRepositoryAsync<User>()
                    .FirstOrDefaultAsync(predicate: a => a.Id == refreshToken.UserId);
                if (user == null)
                    throw new ApplicationException(ExceptionConstants.InValidToken);

                if (refreshToken.RevokedOn == null && DateTime.UtcNow <= refreshToken.ExpiredOn.UtcDateTime)
                    throw new ApplicationException(ExceptionConstants.ActiveToken);

                refreshToken.RevokedOn = DateTimeOffset.UtcNow;
                var newRefreshToken = GenerateRefreshToken();
                newRefreshToken.UserId = user.Id;

                var refreshTokenRep = _unitOfWork.GetRepositoryAsync<RefreshToken>();
                var getAllLastTokens = await refreshTokenRep.GetListAsync(a => a.UserId == user.Id);
                if (getAllLastTokens.Count() > 0)
                    refreshTokenRep.Delete(getAllLastTokens);

                await refreshTokenRep.AddAsync(newRefreshToken);
                
                var claims = await CreateClaims(user);
                var claimList = await GetPermissionsAsync(claims.Role, Platform.Android);
                claimList.AddRange(claims.Claims);
                var jwt = await CreateJwtTokenAsync(claimList);
                string newToken = new JwtSecurityTokenHandler().WriteToken(jwt);
                
                var accessTokens = await _unitOfWork.GetRepositoryAsync<AccessToken>()
                    .GetListAsync(predicate: a => a.UserId == user.Id && a.LoginProvider == AppConstants.Login, disableTracking: false);

                var accessTokenRepository = _unitOfWork.GetRepositoryAsync<AccessToken>();

                if (accessTokens.Count() > 0)
                    accessTokenRepository.Delete(accessTokens);

                await accessTokenRepository.AddAsync(new AccessToken()
                {
                    UserId = user.Id,
                    LoginProvider = AppConstants.Login,
                    Name = AppConstants.Token,
                    Value = newToken
                });
                await _unitOfWork.SaveChangesAsync();

                RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO()
                {
                    RefreshTokenExpiration = newRefreshToken.ExpiredOn,
                    RefreshTokenKey = newRefreshToken.Token,
                    Token = newToken
                };
                return new APIResult { state = true, entity = refreshTokenDTO };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        public async Task<APIResult> CheckCode(ForgotPasswordDTO forgetPassword)
        {
            try
            {
                var user = await _userService.GetUserByEmail(forgetPassword.Email);
                if (user == null)
                    throw new ApplicationException(ExceptionConstants.WrongEmail);

                var userTokenRepository = _unitOfWork.GetRepositoryAsync<AccessToken>();
                var getCodeToken = await userTokenRepository.FirstOrDefaultAsync(a => a.LoginProvider == AppConstants.ForgetPassword && a.Name == AppConstants.Code && a.UserId == user.Id);
                CheckCodeResponse checkCodeDTO = new CheckCodeResponse()
                {
                    Code = forgetPassword.Code,
                    Email = forgetPassword.Email,
                    Token = forgetPassword.Token
                };
                if (getCodeToken == null || getCodeToken.Value != forgetPassword.Code.ToString())
                    checkCodeDTO.IsSuccessCode = false;
                else
                    checkCodeDTO.IsSuccessCode = true;

                return new APIResult { state = true, entity = checkCodeDTO };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        public async Task<APIResult> Logout(string token)
        {
            try
            {
                var accessToken = await _unitOfWork.GetRepositoryAsync<AccessToken>()
                    .FirstOrDefaultAsync(predicate: a => a.Value == token, disableTracking: false);

                if (accessToken != null)
                {
                    var accessTokenRepository = _unitOfWork.GetRepositoryAsync<AccessToken>();
                    accessTokenRepository.Delete(accessToken);
                }

                var refreshToken = await _unitOfWork.GetRepositoryAsync<RefreshToken>()
                    .FirstOrDefaultAsync(predicate: a => a.UserId == _context.GetUserId() && a.Token == token, disableTracking: false);

                if (refreshToken != null)
                {
                    var refreshTokenRepository = _unitOfWork.GetRepositoryAsync<RefreshToken>();
                    refreshTokenRepository.Delete(refreshToken);
                }
                await _unitOfWork.SaveChangesAsync();

                return new APIResult { state = true };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


        #region Private Methods
        private async Task<List<Claim>> GetPermissionsAsync(string RoleUser, Platform platform)
        {
            var claims = new List<Claim>();
            var role = await _roleService.GetRoleByName(RoleUser);
            if (role != null)
            {
                var roleClaims = await _roleClaimService.GetRoleClaim(role.Id, platform);
                foreach (var claim in roleClaims)
                {
                    claims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
                }
            }
            return claims;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiredOn = DateTimeOffset.Now.AddMinutes(_jWTOptions.TokenLifeTime),
                CreatedOn =  DateTimeOffset.Now
            };
        }
        private async Task<JwtSecurityToken> CreateJwtTokenAsync(IEnumerable<Claim> claims) =>
            new JwtSecurityToken(
                issuer: _jWTOptions.Issuer,
                audience: _jWTOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jWTOptions.TokenLifeTime),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTOptions.Key)), SecurityAlgorithms.HmacSha256)
            );
        private async Task<CreateClaimsDTO> CreateClaims(User user)
        {
            var role = await _roleService.GetByIdAsync(user.RoleId);
            var User = await _userService.GetUserById(user.Id); 

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimConstants.Role, role.Name),
                new Claim(ClaimConstants.UserId, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email??""),
                new Claim(ClaimConstants.PhoneNumber, user.PhoneNumber)
            };
            CreateClaimsDTO createClaims = new CreateClaimsDTO()
            {
                Claims = claims,
                Role = role.Name
            };
            return createClaims;
        }
        private async Task<string> DeleteCode(User user)
        {
            var userTokenRepository = _unitOfWork.GetRepositoryAsync<AccessToken>();
            var getCodeToken = await userTokenRepository.FirstOrDefaultAsync(a => a.LoginProvider == AppConstants.ForgetPassword && a.Name == AppConstants.Code && a.User.Id == user.Id);
            string code = "";
            if (getCodeToken!=null)
            {
                code = getCodeToken.Value;
                userTokenRepository.Delete(getCodeToken);
                await _unitOfWork.SaveChangesAsync();
            }
            return code;
        }

        private async Task<APIResult> SendingMail(User user, string code)
        {
            try
            {
                var emailBody = EmailString(user.Email, MailType.ForgetPassword);
                emailBody = emailBody.Replace("{{employee.email}}", $"{user.Email}");
                emailBody = emailBody.Replace("{{code_number}}", $"{code}");
                await _emailService.SendEmailAsync(new EmailDTO()
                {
                    ToEmail = user.Email,
                    Subject = CoreResources.ResetPassword,
                    Body = emailBody
                });
                return new APIResult { state = true };
            }
            catch (Exception ex)
            {
                return new APIResult { state = false, message = ex.Message };
            }
        }

        private string EmailString(string email, MailType mailType)
        {
            string emailBody = string.Empty;
            if (mailType == MailType.Register)
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'>تم تلقى طلب تسجيلك<br style='margin: 0px; padding: 0px;'></strong><br style='margin: 0px; padding: 0px;'>
                                </div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                نشكر حضراتكم ، تم تلقى طلب تسجيلكم بنجاح<br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                من فضلكم نرجو الانتظار لحين بحث طلبكم ، <br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'> ستصلكم رسالة على البريد الالكترونى و رقم الهاتف بحالة طلبكم عند الرد عليه الى ذلك الحين ، يمكنكم الذهاب الى المنصة للاطلاع على كل جديد لدينا
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>بالتوفيق,<br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>PATHFINDER فريق عمل</strong></div>";
                }
                else
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'>Your registration request has been received<br style='margin: 0px; padding: 0px;'></strong><br style='margin: 0px; padding: 0px;'>
                                </div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                We thank you, your registration request has been received successfully<br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                Please wait while your request is reviewed. <br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'> You will receive a message via email and phone number regarding the status of your request when you respond to it. Until then, you can go to the platform to see everything we have new.
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>Best regards,<br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>PATHFINDER Team</strong></div>";
                }
            }
            else if (mailType == MailType.AcceptAccountStatus)
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'>تم الموافقة على طلبك<br style='margin: 0px; padding: 0px;'></strong><br style='margin: 0px; padding: 0px;'>
                                </div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                نشكر حضراتكم ، و نعتذر على النتظار<br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                تم الموافقة على حسابك يمكنك الدخول على الموقع الالكترونى من خلال
                                <br style='margin: 0px; padding: 0px;'>
                                https://PATHFINDER-staging.orchtech.com/User/Account/Login
                                <br style='margin: 0px; padding: 0px;'> 
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>بالتوفيق<br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>PATHFINDER فريق عمل</strong></div>";
                }
                else
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'>Your account has been accepted<br style='margin: 0px; padding: 0px;'></strong><br style='margin: 0px; padding: 0px;'>
                                </div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                We thank you, and apologize for the wait<br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                Your account has been approved. You can log in to the website through 
                                <br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'> 
                                https://PATHFINDER-staging.orchtech.com/User/Account/Login
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>Best regards,<br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>PATHFINDER Team</strong></div>";
                }
            }
            else if (mailType == MailType.RejectAccountStatus)
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'>تم رفض طلبك<br style='margin: 0px; padding: 0px;'></strong><br style='margin: 0px; padding: 0px;'>
                                </div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                نشكر حضراتكم ، و نعتذر على النتظار<br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                تم رفض حسابك يمكنك الرجوع الى الأدمن
                                <br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'> 
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>بالتوفيق<br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>PATHFINDER فريق عمل</strong></div>";
                }
                else
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'>Your account has been rejected<br style='margin: 0px; padding: 0px;'></strong><br style='margin: 0px; padding: 0px;'>
                                </div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                We thank you, and apologize for the wait<br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                Your account has been rejected. You can return to admin
                                <br style='margin: 0px; padding: 0px;'>
                                <br style='margin: 0px; padding: 0px;'> 
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>Best regards,<br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>PATHFINDER Team</strong></div>";
                }
            }
            else if (mailType == MailType.ForgetPassword)
            {
                if (Thread.CurrentThread.CurrentCulture.Name == "ar-EG")
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, 
                                sans-serif;'><strong style='font-weight: bold; margin: 0px; padding: 0px;'>إعادة تعيين كلمة المرور <br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;
                                Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>لقد طلبت إعادة تعيين كلمة المرور، يرجى إدخال الأرقام أدناه لإعادة تعيين كلمة المرور الخاصة بك.
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); 
                                font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'><strong style='font-weight: bold; margin: 0px; padding: 0px;'>
                                Code Number:</strong>&nbsp;<em style='margin: 0px; padding: 0px;'>{{code_number}}\r\n<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); 
                                font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'><br style='margin: 0px; padding: 0px;'><br style='margin: 0px; 
                                padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica,     
                                Arial, sans-serif;'>تجاهل هذه الرسالة للاحتفاظ بكلمة المرور الحالية.<br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>Best regards,<br style='margin: 0px; padding: 0px;'>
                                </strong><br style='margin: 0px; padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;
                                Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'><strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; 
                                padding: 0px;'>PATHFINDER Team</strong></div>";
                }
                else
                {
                    emailBody = @"<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, 
                                sans-serif;'><strong style='font-weight: bold; margin: 0px; padding: 0px;'>Reset Password <br style='margin: 0px; padding: 0px;'></strong>
                                <br style='margin: 0px; padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;
                                Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>You have requested a password reset, please enter the numbers below to reset your password.
                                <br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); 
                                font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'><strong style='font-weight: bold; margin: 0px; padding: 0px;'>
                                Code Number:</strong>&nbsp;<em style='margin: 0px; padding: 0px;'>{{code_number}}\r\n<div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); 
                                font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'><br style='margin: 0px; padding: 0px;'><br style='margin: 0px; 
                                padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica,     
                                Arial, sans-serif;'>Ignore this message to keep your current password.<br style='margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'></div>
                                <div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'>
                                <strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; padding: 0px;'>Best regards,<br style='margin: 0px; padding: 0px;'>
                                </strong><br style='margin: 0px; padding: 0px;'></div><div style='margin: 0px; padding: 0px; color: rgb(92, 92, 92); font-family: PATHFINDER, &quot;
                                Helvetica Neue&quot;, Helvetica, Arial, sans-serif;'><strong style='font-weight: bold; margin: 0px; padding: 0px;'><br style='margin: 0px; 
                                padding: 0px;'>PATHFINDER Team</strong></div>";
                }
            }
            emailBody = emailBody.Replace("{{employee.email}}", $"{email}");

            return emailBody;
        }
        #endregion
    }
}
