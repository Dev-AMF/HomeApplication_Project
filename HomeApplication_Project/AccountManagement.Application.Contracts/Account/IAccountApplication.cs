
using _0_Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Login(Login command);
        void Logout();

        EditAccount GetDetails(int id);

        AccountViewModel GetAccountBy(int id);
        OperationResult Create(CreateAccount command);
        OperationResult Register(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        List<AccountViewModel> GetAccounts();
        List<AccountViewModel> Search(AccountSearchModel searchModel);

    }
}
