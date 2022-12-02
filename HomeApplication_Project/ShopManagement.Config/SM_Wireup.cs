using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts.CartServices;
using Query.Contracts.Comment;
using Query.Contracts.Product;
using Query.Contracts.ProductCategory;
using Query.Contracts.ProductPictureSlider;
using Query.Contracts.Slide;
using Query.Queries;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductPictureSliderAgg;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Config.Permissions;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.AccountACL;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagement.Infrastructure.InventoryACL;
using System;

namespace ShopManagement.Config
{
    public static class SM_Wireup
    {
        public static void DoConfig(IServiceCollection services, string ConnectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();


            services.AddTransient<IProductPictureSliderApplication, ProductPictureSliderApplication>();
            services.AddTransient<IProductPictureSliderRepository, ProductPictureSliderRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            services.AddTransient<ISlideQuery, SlideQuery>();

            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();
            services.AddTransient<IProductPictureSliderQuery, ProductPictureSliderQuery>();


            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IPaymentMethodApplication<int , PaymentMethod>, PaymentMethodApplication>();

            
            services.AddTransient<IShopInventoryACL, ShopInventoryACL>();
            services.AddTransient<IShopAccountAcl, ShopAccountACL>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();

            services.AddTransient<ICartCalculatorService, CartCalculatorService>();
            services.AddSingleton<ICartService, CartService>();

            services.AddDbContext<At_HomeApplicationContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        }
    }
}
