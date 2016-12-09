using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.Models;

namespace MvcClient
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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication();
            //services.AddIdentityServer();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            //}
            //else
            //{
            //app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            //change claim type map to jwt claims type
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions()
            {
                Authority = "http://localhost:9090",
                AuthenticationScheme = "oidc",

                ClientId = "mvc-client-one",
                SaveTokens = true,
                SignInScheme = "Cookies",
                ClientSecret = "mvc".Sha256(),
                //CallbackPath = "/home/index",
                RequireHttpsMetadata = false
                ,

                //Scope = {
                //        "MVC.ADMIN",
                //        "MVC.USER",
                //        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServer4.IdentityServerConstants.StandardScopes.Profile},

                //TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                //{
                //    NameClaimType = JwtClaimTypes.Name,
                //    RoleClaimType = JwtClaimTypes.Role
                //}
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
