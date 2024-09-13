
using Microsoft.EntityFrameworkCore;
using PathFinder.Core.Interface.IService;
using PathFinder.Core.Interfaces.Shared.Repository;
using PathFinder.Core.Entities;

namespace PathFinder.BusinessLogic.Services.Shared
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork,
            IRoleService roleService)
        {
            _unitOfWork = unitOfWork;
        }    
        public async Task<User> GetUserByNameOrEmailOrMobile(string userNameOrEmailOrMobile)
        {
            var userRepository = _unitOfWork.GetRepositoryAsync<User>();
            var user = userNameOrEmailOrMobile.ToUpper();
            return await userRepository.FirstOrDefaultAsync(a => a.Email == user || a.PhoneNumber == user,
                disableTracking: false, include: x => x.Include(x => x.Role));
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var userRepository = _unitOfWork.GetRepositoryAsync<User>();
            var user = email.ToUpper();
            return await userRepository.FirstOrDefaultAsync(a => a.Email == user);
        }
        public async Task<User> GetUserById(int userId)
        {
            var userRepository = _unitOfWork.GetRepositoryAsync<User>();
            return await userRepository.FirstOrDefaultAsync(a => a.Id == userId, include: a => a.Include(src => src.Role));
        }
    }
}
