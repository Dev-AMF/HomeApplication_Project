using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using Account.Management.Infrastructure.EFCore;
using BlogManagement.Infrastructure.Config;
using CommentManagement.Infrastructure.Config;
using DiscountManagement.Infrastructure.Config;
using InventoryManagement.Infrastructure.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceHost.Helpers;
using ShopManagement.Config;

namespace ServiceHost
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
            services.AddHttpContextAccessor();

            SM_Wireup.DoConfig(services , Configuration.GetConnectionString("HomeApplicationContext"));
            DM_Wireup.DoConfig(services, Configuration.GetConnectionString("HomeApplicationDiscountContext"));
            IM_Wireup.DoConfig(services, Configuration.GetConnectionString("At_HomeApplicationInventoryContext"));
            BM_Wireup.DoConfig(services, Configuration.GetConnectionString("At_HomeApplicationBlogContext"));
            CM_Wireup.DoConfig(services, Configuration.GetConnectionString("At_HomeApplicationCommentContext"));
            AM_Wireup.DoConfig(services, Configuration.GetConnectionString("At_HomeApplicationAccountContext"));

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            services.AddTransient<IFileUploader, FileUploader>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IAuthHelper, AuthHelper>();

            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => {
                    o.LoginPath = new PathString("/Users");
                    o.LogoutPath = new PathString("/Users");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea",
                builder => builder.RequireRole(new List<string> { Roles.Administator, Roles.SystemUser }));

                options.AddPolicy("Shop",
                builder => builder.RequireRole(new List<string> { Roles.Administator }));

                options.AddPolicy("Discount",
                builder => builder.RequireRole(new List<string> { Roles.Administator }));

                options.AddPolicy("Account",
                builder => builder.RequireRole(new List<string> { Roles.Administator }));
            });

            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");

                    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
