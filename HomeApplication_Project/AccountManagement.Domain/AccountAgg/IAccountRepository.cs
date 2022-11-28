using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<int, Account>
    {
        Account GetBy(string username);
        EditAccount GetDetails(int id);
        List<AccountViewModel> GetAccounts();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
