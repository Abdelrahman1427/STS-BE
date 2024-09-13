using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PathFinder.BusinessLogic.Services.IClient;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.Core.Interface.IService;
using PathFinder.Core.Interface.Shared.IServices;
using PathFinder.Core.Interface.IClientServices;
using PathFinder.BusinessLogic.Services.IUser;
using PathFinder.Core.Interface.IService.IUserServices;
using PathFinder.Core.Interface.IUserServices;

namespace PathFinder.Infrastructure
{
    internal static class ServicesResolver
    {
        internal static void ResolveDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IRoleClaimService, RoleClaimService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEncryptEnginService, EncryptEnginService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailSettingsService, EmailSettingsService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAssessmentService, AssessmentService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICompanyCategoryService, CompanyCategoryService>();
            services.AddScoped<ICompanySectorService, CompanySectorService>();
            services.AddScoped<ICompanyStatusService, CompanyStatusService>();
            services.AddScoped<IDisabilityService, DisabilityService>();
            services.AddScoped<IEducationLevelService, EducationLevelService>();
            services.AddScoped<IEnterpriseOriginService, EnterpriseOriginService>();
            services.AddScoped<IEnterpriseTypeService, EnterpriseTypeService>();
            services.AddScoped<IGovernorateService, GovernorateService>();
            services.AddScoped<IInterventionService, InterventionService>();
            services.AddScoped<IObjectivesService, ObjectiveService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IWorkplaceEnvironmentService, WorkplaceEnvironmentService>();
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEngagementService, EngagementService>();
            services.AddScoped<IEngagementLevelService, EngagementLevelService>();
            services.AddScoped<IEngagementModalityService, EngagementModalityService>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IBeneficiariesService, BeneficiariesService>();
            services.AddScoped<ITOTService, TOTService>();
            services.AddScoped<ICourseTransactionService, CourseTransactionService>();
            services.AddScoped<INGOService, NGOService>();
            services.AddScoped<ICDAService, CDAService>();

            services.AddScoped<ICertificationService, CertificationService>();

            services.AddScoped(typeof(IGetViewUpdateCrudService<>), typeof(GetViewUpdateCrudService<>));
        }
    }
}
