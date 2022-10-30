using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountMangement.Infrastructure;
using AccountMangement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Account.Management.Infrastructure.EFCore
{
    public static class AM_Wireup
    {
        public static void DoConfig(IServiceCollection services, string ConnectionString)
        {

            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<At_HomeApplicationAccountContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        }
    }
}
