using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        void Signin(AuthViewModel account);
        string CurrentAccountRole();
        List<int> GetPermissions();
        AuthViewModel CurrentAccountInfo();
    }
}
