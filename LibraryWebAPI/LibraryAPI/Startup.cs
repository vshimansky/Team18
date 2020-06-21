using LibraryAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Mail;
using UniversalAcceptanceLibrary;

namespace LibraryAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IEmailValidator, EmailValidator>();
            services.AddSingleton<IEmailFormatter, EmailFormatter>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<ITLDChecker, TLDChecker>();
            services.AddSingleton<IUrlFormatter, UrlFormatter>();

            services.AddTransient(sp => {
                var smtpLogin = ""; // Environment.GetEnvironmentVariable("SMTP_LOGIN");
                var smtpPassword = ""; // Environment.GetEnvironmentVariable("SMTP_PASSWORD");

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpLogin, smtpPassword),
                    EnableSsl = true
                };

                return smtpClient;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
