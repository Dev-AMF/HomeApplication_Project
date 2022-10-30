//using AccountManagement.Application;
//using AccountManagement.Application.Contracts.Account;
//using AccountManagement.Domain.AccountAgg;
//using AccountMangement.Infrastructure.EFCore;
//using AccountMangement.Infrastructure.EFCore.Repository;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;

//namespace Account.Management.Infrastructure.EFCore
//{
//    public static class fr_Wireup
//    {
//        public static void DoConfig(IServiceCollection services, string ConnectionString)
//        {

//            services.AddTransient<IAccountApplication, AccountApplication>();
//            services.AddTransient<IAccountRepository, AccountRepository>();

//            //services.AddTransient<IRoleApplication, RoleApplication>();
//            //services.AddTransient<IRoleRepository, RoleRepository>();

//            services.AddDbContext<At_HomeApplicationAccountContext>(
//                options =>
//                {
//                    options.UseSqlServer(ConnectionString);
//                });
//        }
//    }
//}
