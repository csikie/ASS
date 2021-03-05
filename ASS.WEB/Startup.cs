using ASS.BLL.Services;
using ASS.Common.Settings;
using ASS.DAL;
using ASS.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ASS.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string appMode = Configuration.GetSection("Mode").Value;
            IdentitySettings identitySettings = Configuration.GetSection("IdentitySettings").GetSection(appMode).Get<IdentitySettings>();

            // Lokalizáció hozzáadása
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            services.AddMvc()
                    .AddViewLocalization();
            //.AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("hu-HU"),
                    new CultureInfo("en-US"),
                };
                options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddDbContext<ASSContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            // Dependency injection beállítása az authentikációhoz
            services.AddIdentity<User, IdentityRole<int>>()
                    .AddEntityFrameworkStores<ASSContext>() // EF használata a ASSContext entitás kontextussal
                    .AddDefaultTokenProviders(); // Alapértelmezett token generátor használata (pl. SecurityStamp-hez)

            services.Configure<IdentityOptions>(options =>
            {
                // Jelszó komplexitására vonatkozó konfiguráció
                options.Password.RequireDigit = identitySettings.Password.RequireDigit;
                options.Password.RequiredLength = identitySettings.Password.RequiredLength;
                options.Password.RequireNonAlphanumeric = identitySettings.Password.RequireNonAlphanumeric;
                options.Password.RequireUppercase = identitySettings.Password.RequireUppercase;
                options.Password.RequireLowercase = identitySettings.Password.RequireLowercase;
                options.Password.RequiredUniqueChars = identitySettings.Password.RequiredUniqueChars;

                // Hibás bejelentkezés esetén az (ideiglenes) kizárásra vonatkozó konfiguráció
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.Lockout.DefaultLockoutTimeSpanInMins);
                options.Lockout.MaxFailedAccessAttempts = identitySettings.Lockout.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identitySettings.Lockout.AllowedForNewUsers;

                // Felhasználókezelésre vonatkozó konfiguráció
                options.User.RequireUniqueEmail = identitySettings.User.RequireUniqueEmail;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Home/Login";
                options.AccessDeniedPath = new PathString("/Home/Error");
            });

            services.AddScoped<AdminService>();
            services.AddScoped<TeacherService>();
            services.AddScoped<InstructorService>();
            services.AddScoped<StudentService>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15); // max. 15 percig él a munkamenet
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
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

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(serviceProvider, userManager);
        }
    }
}
