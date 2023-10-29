using HairSalon.Data;
using HairSalon.Data.Entities;
using HairSalon.Data.SeedData;
using HairSalon.Web.Services;
using HairSalon.Web.Settings;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using HairSalon;
using HairSalon.Bll.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace HairSalon.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // Add services to the container.
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<DutyTimeService>();
            builder.Services.AddScoped<EmployedService>();
            builder.Services.AddScoped<HairSalonUserService>();
            builder.Services.AddScoped<PictureService>();
            builder.Services.AddScoped<ShopServiceService>();

            builder.Services.AddDbContext<HairSalonDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

            builder.Services.AddIdentity<HairSalonUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<HairSalonDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources.hu");

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration["AZURE_REDIS_CONNECTIONSTRING"];
                options.InstanceName = "SampleInstance";
            });

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "AppointentInputModel";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "AppointentUserInputModel";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 8;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

                options.LoginPath = "/Identity/Accaount/Login";
                options.AccessDeniedPath = "/Identity/Accaount/AccessDenied";
                options.SlidingExpiration = true;
            });

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 256 * 1024 * 1024;
            });

            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodyBufferSize = 256 * 1024 * 1024;
            });


            builder.Services.Configure<EmailSettings>(configuration.GetSection("EmaiSettings"));
            builder.Services.AddTransient<IEmailSender, EmailSender>();


            builder.Services.Configure<UserRoleSettings>(configuration.GetSection("UserRoleSettings"));

            builder.Services.AddScoped<IRoleSeedService, RoleSeedService>();
            builder.Services.AddScoped<IUserSeedService, UserSeedService>();


            builder.Services.AddAuthentication().AddGoogle(option =>
            {
                option.ClientId = configuration["Authentication:Google:ClientId"];
                option.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });

            builder.Services.AddAuthorization(optios =>
            {
                optios.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Administrators"));
                optios.AddPolicy("RequireAdministratorRol", policy => policy.RequireRole("Administrators"));
                optios.AddPolicy("RequireAszisztensRol", policy => policy.RequireRole("Aszisztens"));
                optios.AddPolicy("RequirePrivilegedRol", policy => policy.RequireRole("Privileged"));
            });

            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin", "RequireAdministratorRol");
                //options.Conventions.AuthorizeFolder("/Admin", "RequireAszisztensRol");
                //options.Conventions.AuthorizeFolder("/Admin", "RequirePrivilegedRol");
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Data.HairSalonDbContext>();
                var migrations = dbContext.Database.GetMigrations().ToHashSet();
                if (dbContext.Database.GetAppliedMigrations().Any(a => !migrations.Contains(a)))
                    throw new InvalidOperationException("Az adatb�zison m�r van olyan migr�ci� futtatva, ami az�ta t�r�ltek a projektb�l. T�r�ld az adatb�zist vagy jav�stsd a migr�ci�k �llapot�t, majd indisd �jra az alkalmaz�st!");
                dbContext.Database.Migrate();

                var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleSeedService>();
                await roleSeeder.SeedRoleAsync();

                var userSeeder = scope.ServiceProvider.GetRequiredService<IUserSeedService>();
                await userSeeder.SeedUserAsync();
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}