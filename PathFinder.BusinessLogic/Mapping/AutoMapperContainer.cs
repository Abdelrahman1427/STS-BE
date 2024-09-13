using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace PathFinder.BusinessLogic.Mapping
{
    public static class AutoMapperContainer
    {
        private static IHttpContextAccessor _context;
        public static IMapper CreateMapper(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new NotificationProfile());
                cfg.AddProfile(new CertificationProfile());
                cfg.AddProfile(new CityProfile());
                cfg.AddProfile(new CompanyCategoryProfile());
                cfg.AddProfile(new CompanyStatusProfile());
                cfg.AddProfile(new CompanySectorProfile());
                cfg.AddProfile(new DisabilityProfile());
                cfg.AddProfile(new EducationLevelProfile());
                cfg.AddProfile(new EmployeeProfile());
                cfg.AddProfile(new EngagementLevelProfile());
                cfg.AddProfile(new EngagementProfile());
                cfg.AddProfile(new EnterpriseOriginProfile());
                cfg.AddProfile(new EnterpriseTypeProfile());
                cfg.AddProfile(new GovernorateProfile());
                cfg.AddProfile(new InterventionProfile());
                cfg.AddProfile(new ObjectivesProfile());
                cfg.AddProfile(new PartnerProfile());
                cfg.AddProfile(new PolicyProfile());
                cfg.AddProfile(new RoleProfile());
                cfg.AddProfile(new UserTypeProfile());
                cfg.AddProfile(new WorkplaceEnvironmentProfile());
                cfg.AddProfile(new CourseTransactionProfile());
                cfg.AddProfile(new BeneficiarieProfile());
                cfg.AddProfile(new AssessmentProfile());
                cfg.AddProfile(new AssessmentProfile());
                cfg.AddProfile(new NGOProfile());
                cfg.AddProfile(new CDAProfile());

            }).CreateMapper();
        }
    }
}