using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using VBSAdmin.Data;
using VBSAdmin.Models;
using VBSAdmin.Services;
using VBSAdmin.Authorization;
using WebApiContrib.Core.Formatter.Csv;
using Microsoft.AspNetCore.Mvc;

namespace VBSAdmin
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
            // Add our custom authorization handler.
            services.AddSingleton<IAuthorizationHandler, BelongsToTenantHandler>();


            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddMvc();
            services.AddMvc().AddCsvSerializerFormatters(new CsvFormatterOptions { CsvDelimiter = "," });
            services.AddMvc(options => options.EnableEndpointRouting = false);
         

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SystemAdminOnly", policy => policy.RequireClaim("SystemAdmin"));
                options.AddPolicy("TenantAdmin", policy => policy.RequireClaim("TenantAdmin"));
                options.AddPolicy("BelongToTenant", policy => policy.Requirements.Add(new Authorization.BelongsToTenantRequirement()));
            }); 

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
