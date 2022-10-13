using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.ColleagueAgg;
using DiscountManagement.Application.Contracts.CustomerAgg;
using DiscountManagement.Domain.ColleagueAgg;
using DiscountManagement.Domain.CustomerAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiscountManagement.Infrastructure.Config
{
    public static class DM_Wireup
    {
        public static void DoConfig(IServiceCollection services, string ConnectionString)
        {
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();

            
            services.AddDbContext<At_HomeApplicationDiscountContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        }
    }
}