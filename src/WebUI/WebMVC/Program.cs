using AutoMapper;
using Domain.Entities.Identity;
using Domain.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Persistence.ContextModel;
using Serilog;
using Serilog.Events;
using Utility.Factory;
using WebMVC.Mapping;
using WebMVC.Models.IdentityModels;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string date = DateTime.Now.ToString("dd-MMM-yyyy");
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));
try
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Persistence")));

    //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services
        .AddIdentity<ApplicationUser, ApplicationRole>()
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddClaimsPrincipalFactory<ApplicationClaimsPrincipalFactory>()
        .AddDefaultTokenProviders();


    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 0;

        // Lockout settings
        //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;

        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    });

    builder.Services.Configure<PasswordHasherOptions>(options =>
        options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
    );

    builder.Services.AddAuthorization();

    builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();


    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new AppAutoMapperProfile());
        mc.AddProfile(new MappingProfile());
    });

    var mapper = mappingConfig.CreateMapper();

    builder.Services.AddSingleton(mapper);

    var dependency = new DependencyResolver();

    dependency.SetDependencyConfiguration(builder.Services);

    builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

    builder.Services.AddMemoryCache();

    builder.Services.Configure<FormOptions>(options =>
    {
        options.MultipartBodyLengthLimit = 4294967296; // Set the value to your desired maximum size in bytes
    });

    builder.Services.Configure<IISServerOptions>(options =>
    {
        options.MaxRequestBodySize = int.MaxValue;
    });

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    Log.Information("Application Starting.");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    var staticFileOptions = new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), Domain.Utility.Utility.UploadingFolderPath)),
        RequestPath = new PathString(configuration[Domain.Utility.Utility.UploadingFolderPath])
    };
    app.UseStaticFiles(staticFileOptions);
    if (app.Environment is IWebHostEnvironment env)
    {
        Domain.Utility.Utility.ServerPath = env.WebRootPath;
    }
    Domain.Utility.Utility.ProjectPhysicalPath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), Domain.Utility.Utility.UploadingFolderPath)).Root;

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
           name: "default",
           pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}

catch (Exception ex)
{
    Log.Fatal(ex, "Application crashed");
}
finally
{
    Log.CloseAndFlush();
}