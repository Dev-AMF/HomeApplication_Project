using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Domain;
using Account.Management.Infrastructure.EFCore;
using BlogManagement.Infrastructure.Config;
using BlogManagement.Presentation.Api;
using CommentManagement.Infrastructure.Config;
using DiscountManagement.Infrastructure.Config;
using InventoryManagement.Infrastructure.Config;
using InventoryManagement.Presentation.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parbad.Builder;
using Parbad.Gateway.Mellat;
using Parbad.Gateway.ParbadVirtual;
using ServiceHost.Helpers;
using ServiceHost.PageFilters;
using ShopManagement.Config;
using ShopManagement.Presentation.Api;

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
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Users");
                    o.LogoutPath = new PathString("/Users");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea",
                builder => builder.RequireRole(new List<string> { Roles.Administator, Roles.SystemUser }));

                options.AddPolicy("Shop",
                builder => builder.RequireRole(new List<string> { Roles.Administator, Roles.SystemUser }));

                options.AddPolicy("Discount",
                builder => builder.RequireRole(new List<string> { Roles.Administator }));

                options.AddPolicy("Account",
                builder => builder.RequireRole(new List<string> { Roles.Administator }));
            });

            services.AddRazorPages()
                .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");

                    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
                })
                .AddApplicationPart(typeof(ProductController).Assembly)
                .AddApplicationPart(typeof(InventoryController).Assembly)
                .AddApplicationPart(typeof(ArticleController).Assembly)
                .AddNewtonsoftJson()
            ;

            services.AddParbad()
                .ConfigureGateways(gateways =>
                {
                    gateways
                         .AddMellat()
                         .WithAccounts(source =>
                         {
                             source.AddInMemory(account =>
                             {
                                 account.TerminalId = 123;
                                 account.UserName = "MyId";
                                 account.UserPassword = "MyPassword";
                             });
                         });

                    gateways.AddParbadVirtual()
                            .WithOptions(options => options.GatewayPath = "/MyvirtualGateway");

                })
                .ConfigureHttpContext(builder => builder.UseDefaultAspNetCore())
                .ConfigureStorage(builder => builder.UseMemoryCache())
                .ConfigureAutoRandomTrackingNumber(options =>
                 {
                     options.MinimumValue = 1000;
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
            
            app.UseParbadVirtualGateway();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                //endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
