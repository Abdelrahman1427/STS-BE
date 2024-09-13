using PathFinder.WebApp.Clients;
using PathFinder.WebApp.SubscribeTableDependencies;
using PathFinder.WebApp.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PathFinder.BusinessLogic.Mapping;
using PathFinder.BusinessLogic.Services.Shared;
using PathFinder.Infrastructure.DBContexts;
using PathFinder.SharedKernel.Helpers.Utilties;
using PathFinder.SharedKernel.Polices;
using System.Globalization;
using System.Net.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();
builder.Services.AddMvcCore().AddDataAnnotationsLocalization();

builder.Services.AddControllersWithViews();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("UserDashboard");
builder.Services.AddDbContext<PathFinderDBContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton);

var dateFormat = new DateTimeFormatInfo
{
    ShortDatePattern = "dd/MM/yyyy",
    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
};
var en = new CultureInfo("en-US");
var ar = new CultureInfo("ar-EG");
en.DateTimeFormat = dateFormat;
ar.DateTimeFormat = dateFormat;

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new[] { en, ar };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

builder.Services.AddHttpClient<IHttpRequestClient, HttpRequestClient>()
    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
    {
        MaxConnectionsPerServer = 100,
        PooledConnectionLifetime = TimeSpan.FromMinutes(5),
        PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
        SslOptions = new SslClientAuthenticationOptions
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            }
        }
    })
    .SetHandlerLifetime(TimeSpan.FromMinutes(6));

// Add services to the container.
builder.Services.AddTransient<IPolicyUtils, PolicyUtils>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();
builder.Services.AddSingleton<NotificationHub>();

#region Session
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
#endregion

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.SlidingExpiration = false;
        options.AccessDeniedPath = "/account/AccessDenied/";
        options.LoginPath = "/account/Login";
        options.LogoutPath = "/account/Logout";
    });

builder.Services.AddAuthorization(options => Polices.AddPolices(options));

// builder.WebHost.UseSentry();

#region autoMapper
builder.Services.AddSingleton(AutoMapperContainer.CreateMapper);
#endregion

builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 102400000;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
// app.UseSentryTracing();
app.UseAuthorization();

app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();



app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);

app.Run();
