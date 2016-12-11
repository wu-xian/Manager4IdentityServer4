using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper;
using IdentityServer4_Manager.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityServer4_Manager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add host services
            AddServices(services);

            services.AddDistributedMemoryCache();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<Model.IdentityDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped
            );

            services.AddIdentity<Model.IdentityUser,
                Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole>()
                .AddEntityFrameworkStores<Model.IdentityDbContext>()
                ;

            //Add IdentityServer services
            AddIdentityServer(services);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //initialize DB
            InitializeDatabase(app);

            //automapper
            AutoMapper();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookies",
                LoginPath = "/account/login"
            });

            app.UseIdentity();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<UserLoginService>();
            services.AddScoped<ClientService>();
        }

        private void AddIdentityServer(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer(config =>
            {

            })
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder =>
                builder.UseMySql(Configuration.GetConnectionString("DefaultConnection"), options =>
                 options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                builder.UseMySql(Configuration.GetConnectionString("DefaultConnection"), options =>
                 options.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<Model.IdentityUser>()
                ;
        }

        private void InitializeDatabase(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                configContext.Database.Migrate();

                var identityContext = serviceScope.ServiceProvider.GetRequiredService<Model.IdentityDbContext>();
                identityContext.Database.Migrate();

                if (!configContext.ApiResources.Any())
                {
                    foreach (var item in Config.ApiResources.Get())
                    {
                        configContext.ApiResources.Add(item.ToEntity());
                    }
                }

                if (!configContext.IdentityResources.Any())
                {
                    foreach (var item in Config.IdentityResource.Get())
                    {
                        configContext.IdentityResources.Add(item.ToEntity());
                    }
                }

                if (!configContext.Clients.Any())
                {
                    foreach (var item in Config.Clients.Get())
                    {
                        item.AllowedScopes.Add("wulala");
                        configContext.Clients.Add(item.ToEntity());
                    }
                }
                configContext.SaveChanges();

                ///var userManager = builder.ApplicationServices.GetService<Microsoft.AspNetCore.Identity.UserManager<string>>();
                if (!identityContext.Users.Any())
                {
                    foreach (var item in Config.Users.Get())
                    {
                        identityContext.Users.Add(item);
                        foreach (var item2 in Config.Users.GetClaims())
                        {
                            identityContext.UserClaims.Add(new IdentityUserClaim<string>()
                            {
                                UserId = item.Id,
                                ClaimValue = item2.Value,
                                ClaimType = item2.Type
                            });
                        }
                    }
                }

                if (!identityContext.Roles.Any())
                {
                    foreach (var item in Config.Roles.Get())
                    {
                        identityContext.Roles.Add(item);
                    }
                }
                identityContext.SaveChanges();
            }
        }

        private void AutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                #region IdentityUser => UserDisplay
                cfg.CreateMap<Model.IdentityUser, Model.ViewModel.UserDisplay>()
                    .ForMember(ud => ud.UserClaims, iu => iu.MapFrom(s => s.Claims))
                    .ForMember(d => d.UserRoles, u => u.MapFrom(item => item.Roles))
                    ;
                //IdentityUserClaim => Claim
                cfg.CreateMap<IList<IdentityUserClaim<string>>, IList<Model.Claim>>();
                cfg.CreateMap<IdentityUserClaim<string>, Model.Claim>()
                    .ForMember(d => d.ClaimType, u => u.MapFrom(item => item.ClaimType))
                    .ForMember(d => d.ClaimValue, u => u.MapFrom(item => item.ClaimValue))
                    ;
                //IdentityUserClaim => Claim
                cfg.CreateMap<IList<IdentityUserRole<string>>, IList<Model.UserRole>>();
                cfg.CreateMap<IdentityUserRole<string>, Model.UserRole>()
                    .ForMember(d => d.RoleId, u => u.MapFrom(item => item.RoleId))
                    ;
                #endregion

                #region Security Claim <=> Claim

                cfg.CreateMap<System.Security.Claims.Claim, Model.Claim>()
                    .ForMember(d => d.ClaimType, u => u.MapFrom(item => item.Type))
                    .ForMember(d => d.ClaimValue, u => u.MapFrom(item => item.Value))
                    ;
                cfg.CreateMap<Model.Claim, System.Security.Claims.Claim>()
                    .ForMember(d => d.Type, u => u.MapFrom(item => item.ClaimType))
                    .ForMember(d => d.Value, u => u.MapFrom(item => item.ClaimValue))
                    ;

                #endregion

                #region ??
                cfg.CreateMap<IdentityUser<string>, Model.ViewModel.UserDisplay>();
                cfg.CreateMap<IdentityRole<string>, Model.ViewModel.UserDisplay>();
                cfg.CreateMap<IdentityRoleClaim<int>, Model.Claim>();
                cfg.CreateMap<Model.Claim, IdentityUserClaim<int>>();
                cfg.CreateMap<Model.Claim, IdentityRoleClaim<int>>();
                cfg.CreateMap<IdentityUser<string>, Model.RoleUser>();
                #endregion

                #region Client => ClientDisplay
                cfg.CreateMap<ClientScope, ClientScope>()
            .ForMember(d => d.Client, u => u.Ignore());
                cfg.CreateMap<Client, Model.ViewModel.ClientDisplay>()
                    .ForMember(d => d.ScopeCount, u => u.MapFrom(item => item.AllowedScopes.Count()))
                #endregion

                ;
            });
        }
    }
}
