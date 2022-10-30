using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }

        public AuthViewModel(int id, int roleId, string fullname, string username)
        {
            Id = id;
            RoleId = roleId;
            Fullname = fullname;
            Username = username;
        }
    }
}
