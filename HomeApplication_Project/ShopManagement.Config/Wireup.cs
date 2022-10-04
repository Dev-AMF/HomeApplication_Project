using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Domain.ProductCategoryAgg;
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
            //services.AddTransient<IArticleCategoryValidatorService, ArticleCategoryValidatorService>();

            //services.AddTransient<IArticleApplication, ArticleApplication>();
            //services.AddTransient<IArticleRepository, ArticleRepository>();
            //services.AddTransient<IArticleValidatorService, ArticleValidatorService>();
            //services.AddTransient<IArticleQuery, ArticleQuery>();

            //services.AddTransient<ICommentApplication, CommentApplication>();
            //services.AddTransient<ICommentRepository, CommentRepository>();

            //services.AddTransient<IUnitofwork, UnitofworkEf>();

            services.AddDbContext<At_HomeApplicationContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        }
    }
}
