using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuestionAnswer.DomainModels;
using QuestionAnswer.DomainModels.Interfaces;
using QuestionAnswer.Repositories;
using QuestionAnswer.Repositories.Interfaces;

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
            services.AddControllers();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IAuthDomainModel, AuthDomainModel>();
            services.AddTransient<IMainRepository, MainRepository>();
            services.AddTransient<IMainDomainModel, MainDomainModel>();
            services.AddTransient<ICabinetRepository, CabinetRepository>();
            services.AddTransient<ICabinetDomainModel, CabinetDomainModel>();
            services.AddTransient<INewPollRepository, NewPollRepository>();
            services.AddTransient<INewPollDomainModel, NewPollDomainModel>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:8080");
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                      builder.AllowCredentials();
                                  });
            });

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

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
