using AccountManagement.Application.Contracts.Account;
using ShopManagement.Domain.Services;
using System;

namespace ShopManagement.Infrastructure.AccountACL
{
    public class ShopAccountACL : IShopAccountAcl
    {
        private readonly IAccountApplication _accountApplication;

        public ShopAccountACL(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public AccountViewModel GetAccountBy(int id)
        {
            var account = _accountApplication.GetAccountBy(id);

            return account;
        }
    }
}
