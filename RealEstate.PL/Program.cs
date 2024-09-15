using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using RealEstate.BLL.Interfaces;
using RealEstate.BLL.InterFaces;
using RealEstate.BLL.Repositories;
using RealEstate.BLL;
using RealEstate.PL.Helper;
using RealEstate.PL.Services.Email;
using RealEstate.PL.Services.Localization;
using RealEstate.PL.Services.UploadFile;
using RealEstate.PL.Services;
using System.Globalization;
using RealEstate.DAL;
using RealEstate.PL.Services.NavbarSettings;
using RealEstate.PL.Middleware;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("MailConfigurations"));
builder.Services.AddSingleton<EmailConfiguration>();
builder.Services.Configure<CompanyDetails>(builder.Configuration.GetSection("CompanyDetails"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<CompanyDetails>();
builder.Services.AddControllersWithViews();


builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizationFactory>();
builder.Services.AddMvcCore()
    .AddViewLocalization()
    .AddDataAnnotations();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                    new CultureInfo("ar_EG"),
                    new CultureInfo("en_US"),
                };
    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Configuration.GetConnectionString("RealEstate");
builder.Services.AddDbContext<RealEstateDdContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("RealEstate")); // SQL Server provider
});

builder.Services.AddTransient<DataSeeder>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IVideoService, VideoService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<INavigationService, NavigationService>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(option => option.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<RealEstateDdContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/LogOut";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(2);
});

builder.Services.AddScoped<IEmailService, EmailSender>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/Home/ErrorProd");
    app.UseHsts();


    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            context.Response.Redirect("/Home/ErrorProd");
            await Task.CompletedTask;
        });
    });
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

var supportCuluture = new[] { "ar_EG", "en_US" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportCuluture[0])
    .AddSupportedCultures(supportCuluture[0])
    .AddSupportedUICultures(supportCuluture[0]);

app.UseRequestLocalization(localizationOptions);
app.UseAuthorization();

app.UseAuthorization();
if (!app.Environment.IsDevelopment())
{ 
    app.UseMiddleware<ExceptionMiddleware>(); 
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.Initialize(services);
}
app.Run();
