using PathFinder.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace PathFinder.Infrastructure.DBContexts
{
    public class STSDBContext : DbContext
    {
        private readonly IHttpContextAccessor _context;
        public STSDBContext(DbContextOptions<STSDBContext>options, IHttpContextAccessor context) : base(options)
        {
            _context = context;
        }

        public DbSet<UserType> UserType { get; set; }
        public DbSet<EmailSetting> EmailSetting { get; set; }
        public DbSet<Logger> Logger { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<HubConnection> HubConnection { get; set; }
        public DbSet<AccessToken> AccessToken { get; set; }
        public DbSet<Assessment> Assessment { get; set; }
        public DbSet<Beneficiarie> Beneficiary { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CommunityDevlopmentAssosition> CommunityDevlopmentAssosition { get; set; }
        public DbSet<CompanyCategory> CompanyCategory { get; set; }
        public DbSet<CompanySector> CompanySector { get; set; }
        public DbSet<CompanySize> CompanySize { get; set; }
        public DbSet<CompanyStatus> CompanyStatus { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<CourseTransaction> CourseTransaction { get; set; }
        public DbSet<Disability> Disability { get; set; }
        public DbSet<Documentation> Documentation { get; set; }
        public DbSet<EducationLevel> EducationLevel { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Engagement> Engagement { get; set; }
        public DbSet<EngagementLevel> EngagementLevel { get; set; }
        public DbSet<EngagementModality> EngagementModality { get; set; }
        public DbSet<EnterpriseOrigin> EnterpriseOrigin { get; set; }
        public DbSet<EnterpriseType> EnterpriseType { get; set; }
       public DbSet<FinancialServiceProvider> FinancialServiceProvider { get; set; }
        public DbSet<Governorate> Governorate { get; set; }
        public DbSet<Indicator> Indicator { get; set; }
        public DbSet<IndicatorTransaction> IndicatorTransaction { get; set; }
        public DbSet<Intervention> Intervention { get; set; }
        public DbSet<MessageType> MessageType { get; set; }
        public DbSet<News> New { get; set; }
        public DbSet<NonGovermntalOrgniszation> NonGovermntalOrgniszation { get; set; }
        public DbSet<Objective> Objective { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<PrivateSector> privateSector { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleClaim> RoleClaim { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<SuccessStories> SuccessStory { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WorkplaceEnvironment> WorkplaceEnvironment { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Notification>().ToTable(tb => tb.HasTrigger("tr_dbo_Notification_Sender"));
            base.OnModelCreating(builder);
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            builder.Entity<Role>()
            .Property(b => b.Id)
            .ValueGeneratedNever();
        }
    }
}