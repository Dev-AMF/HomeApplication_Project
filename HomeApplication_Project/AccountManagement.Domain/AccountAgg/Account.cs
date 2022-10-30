using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string MobileNo { get; private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }
        public string ProfilePhoto { get; private set; }


        public Account(string fullname, string username, string password, string mobile,
            int roleId, string profilePhoto)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            MobileNo = mobile;
            RoleId = roleId;
            ProfilePhoto = profilePhoto;
        }

        public void Edit(string fullname, string username, string mobile,
            int roleId, string profilePhoto)
        {
            Fullname = fullname;
            Username = username;
            MobileNo = mobile;
            RoleId = roleId;
            ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        protected Account()
        {

        }
    }
}
