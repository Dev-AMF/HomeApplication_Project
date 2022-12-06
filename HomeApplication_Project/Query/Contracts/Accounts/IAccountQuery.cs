using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Accounts
{
    public interface IAccountQuery
    {
        public int GetUsersCountBy(DateTime date, bool orientation, int roleId);
    }
}
