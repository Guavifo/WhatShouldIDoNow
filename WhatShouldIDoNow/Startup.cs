using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WhatShouldIDoNow.DataAccess;
using WhatShouldIDoNow.Services;
using Microsoft.AspNetCore.Http;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace WhatShouldIDoNow
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            if (env.IsDevelopment())
                { builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); }
            else
                { builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true); }
            
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string devEnv = Configuration["ASPNETCORE_ENVIRONMENT"];
            string connectionString = Configuration.GetConnectionString("WSIDN");

            // data access stuff
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDbConnectionProvider>(new DbConnectionProvider(connectionString));
            services.AddSingleton<IHashingWrapper, HashingWrapper>();
            services.AddScoped<ITaskCommands, TaskCommands>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IUserQueries, UserQueries>();
            services.AddScoped<IUserSignUpService, UserSignUpService>();
            services.AddScoped<IUserCommands, UserCommands>();

            services.AddAuthentication();

            // Add framework services.
            services.AddMvc();
            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = Configuration["Recaptcha:SiteKey"],
                SecretKey = Configuration["Recaptcha:SecretKey"]
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = "/User/SignIn",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
