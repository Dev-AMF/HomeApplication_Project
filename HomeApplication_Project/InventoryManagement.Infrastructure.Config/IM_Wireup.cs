using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.InventoryAgg;
using InventoryManagement.Application.InventoryAgg;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Config.Permissions;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts.Inventory;
using Query.Queries;
using System;

namespace InventoryManagement.Infrastructure.Config
{
    public static class IM_Wireup
    {
        public static void DoConfig(IServiceCollection services, string ConnectionString)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddTransient<IInventoryQuery, InventoryQuery>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

            services.AddDbContext<At_HomeApplicationInventoryContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        }
    }
}