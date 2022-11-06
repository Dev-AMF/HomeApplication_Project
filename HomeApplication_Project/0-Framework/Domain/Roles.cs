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
        public const string ColleagueUser = "4";

        public static string GetRoleBy(int id)
        {
            switch (id)
            {
                case 1:
                    return "کاربر سیستم";
                case 2:
                    return "مدیرسیستم";
                case 3:
                    return "کاربر سایت";
                case 4:
                    return "کاربر همکار";
                default:
                    return "نامشخص";
            }
        }
    }
}
