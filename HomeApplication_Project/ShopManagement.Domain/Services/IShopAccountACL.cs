using AccountManagement.Application.Contracts.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.Services
{
    public interface IShopAccountAcl  
    {
        AccountViewModel GetAccountBy(int id);
    }
}
