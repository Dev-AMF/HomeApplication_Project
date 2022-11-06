﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public static class ApplicationMessages
    {
        public const string RecordAlreadyExists = "Another Record With Name-{0}-Already Exists!,";
        public const string RecordNotFound = "There Is Not Any Records Matching With The Given Information!";
        public const string RecordAlreadyExistsNonArgument = "Another Record With Same Information Already Exists!";
        public const string PasswordNotMatch = "Passwords Don't Match";
        public const string WrongUserOrPass = "Username or Password Is Not Correct!!";
    }
}