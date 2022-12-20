using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class RegexPatterns
    {
        public const string MoblieFormat = @"(\+989|9|09)(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}";
        public const string PasswordFormat = @"^.{6,15}$";
    }
}
