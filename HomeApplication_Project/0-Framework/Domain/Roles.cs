using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Domain
{
    public static class Roles
    {
        public const string SystemUser = "1";
        public const string Administator = "2";
        public const string WebsiteUser = "3";

        public static string GetRoleBy(int id)
        {
            switch (id)
            {
                case 1:
                    return "کاربر سیستم";
                case 3:
                    return "مدیرسیستم";
                default:
                    return "کاربر سایت";
            }
        }
    }
}
