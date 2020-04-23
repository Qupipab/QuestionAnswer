using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuestionAnswer
{
    public class Startup
    {

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:8080");
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                      builder.AllowCredentials();
                                  });
            });
            services.AddControllers();
            services.AddAuthentication(sharedOptions => 
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.Cookie.Name = "AUTHCOOKIE";
                config.Cookie.SameSite = SameSiteMode.None;
                config.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                config.LoginPath = "/SignIn"; ;
                config.AccessDeniedPath = new PathString("/account/login");
                config.ExpireTimeSpan = new TimeSpan(365, 0, 0, 0);
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.SameAsRequest,
                MinimumSameSitePolicy = SameSiteMode.None
            };
            app.UseCookiePolicy(cookiePolicyOptions);

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();  

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
