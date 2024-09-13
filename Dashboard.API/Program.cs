using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.HttpOverrides;
using PathFinder.Infrastructure.Extentions;
using PathFinder.BusinessLogic;
using PathFinder.Infrastructure.DBContexts;
using PathFinder.Infrastructure;
using Serilog;
using STS.API;
using Microsoft.OpenApi.Models;
using PathFinder.SharedKernel.Helpers.Models;
using PathFinder.DataTransferObjects.Helpers;
using PathFinder.BusinessLogic.Mapping;
using PathFinder.SharedKernel.Exceptions;
using PathFinder.SharedKernel.Helpers.Utilties;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.DataTransferObjects.DTOs.Notification;

Log.Logger = new LoggerConfiguration().WriteTo.Console(Serilog.Events.LogEventLevel.Warning)
.CreateBootstrapLogger();
Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    var jsonInputFormatter = options.InputFormatters
        .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
        .Single();
});

#region Json newtonsoft and json patch
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddControllersWithViews(options =>
{
    options.InputFormatters.Insert(0, new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value
        .InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>().First());
});
#endregion

#region Connection String
string clientSTS = builder.Configuration.GetConnectionString("ClientSTS");
builder.Services.AddDbContextClient(clientSTS);
#endregion

#region EmailService
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
#endregion

#region TwilioService
builder.Services.Configure<TwilioSetting>(builder.Configuration.GetSection("Twilio"));
#endregion

#region Flutter
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterOrigins",
    builder =>
    {
        builder.WithOrigins(
            "*"
                            //"http://localhost:4200",
                            //"https://localhost:7193",
                            //"https://PathFinder-staging.orchtech.com:4433/",
                            //"https://PathFinder-staging.orchtech.com/"
                            )
    .AllowAnyHeader()
    .AllowAnyMethod();
    });
});
#endregion

#region JWT Auth
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWTOptions"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
            ValidAudience = builder.Configuration["JWTOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:Key"])),
            ClockSkew = TimeSpan.FromMinutes(30),
            NameClaimType = "email",
            RoleClaimType = "role"
        };
    });
builder.Services.AddAuthorization(options => Polices.AddPolices(options));

#endregion

#region autoMapper
builder.Services.AddSingleton(AutoMapperContainer.CreateMapper);
#endregion

#region localization and globalization
//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddLocalization();

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulturesInfo = CultureUtility.GetSupportedCulturesInfo();
    options.DefaultRequestCulture = new RequestCulture(supportedCulturesInfo[0], supportedCulturesInfo[0]);
    options.SupportedCultures = supportedCulturesInfo;
    options.SupportedUICultures = supportedCulturesInfo;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
});
#endregion

#region autoFac Configure IOC Container for other Projects
//var isDevelopment = builder.Environment.IsDevelopment();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new InfrastructureModule<STSDBContext>());
    builder.RegisterModule(new BusinessLogicModule());

});
#endregion

#region Session
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
#endregion

#region Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v5", new OpenApiInfo
    {
        Title = "My API",
        Version = "v5"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }

  });
    c.OperationFilter<LangParameterOperationFilterUser>();
});
#endregion

#region Sentry
builder.WebHost.UseSentry();
#endregion

#region DependencyInjection
DependencyInjection.ResolveDependencies(builder.Services, builder.Configuration, null);
#endregion

builder.Services.AddSignalR();

#region Run App
var app = builder.Build();
app.UseSession();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v5/swagger.json", name: "Local");
    options.SwaggerEndpoint(url: "/swagger/v5/swagger.json", name: "IIS");
});

app.UseCors("AllowFlutterOrigins");
app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
await MigrateDatabase.EnsureMigration(app);

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/notificationHub");

app.UseMiddleware<ExceptionHandling>();

app.UseSentryTracing();

app.MapControllers();
app.Run();
#endregion