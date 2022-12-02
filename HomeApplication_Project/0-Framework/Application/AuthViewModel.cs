using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string MobileNo { get; set; }
        public List<int> Permissions { get; set; }

        public AuthViewModel(int id, int roleId, string fullname, string username, string mobileNo, List<int> permissions)
        {
            Id = id;
            RoleId = roleId;
            Fullname = fullname;
            Username = username;
            MobileNo = mobileNo;
            Permissions = permissions;
        }
        public AuthViewModel()
        {
                
        }
    }
}
