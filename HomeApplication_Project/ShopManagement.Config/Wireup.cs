using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductPictureSliderAgg;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using System;

namespace ShopManagement.Config
{
    public static class Wireup
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

            //services.AddTransient<IUnitofwork, UnitofworkEf>();

            services.AddDbContext<At_HomeApplicationContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        }
    }
}
