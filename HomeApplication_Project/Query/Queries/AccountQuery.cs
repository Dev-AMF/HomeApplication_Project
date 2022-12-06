using AccountMangement.Infrastructure;
using Query.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class AccountQuery : IAccountQuery
    {
        private readonly At_HomeApplicationAccountContext _accountContext;

        public AccountQuery(At_HomeApplicationAccountContext accountContext)
        {
            _accountContext = accountContext;
        }


        public int GetUsersCountBy(DateTime date, bool orientation, int roleId)
        {
            var query = _accountContext.Accounts.Where(A => A.RoleId == roleId | A.RoleId != roleId);

            if (orientation)
                return query.Where(A => A.CreationDate < date).Count();

            return query.Where(A => A.CreationDate < date).Count();
        }
    }
}
