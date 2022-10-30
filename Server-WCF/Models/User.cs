using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WCF.Models
{
    internal class User
    {
        internal User(string name, int UserToken, UserRole role)
        {
            Name = name;
            Token = UserToken;
            Role = role;
        }
        public string Name { get; set; }
        public int Token { get; }
        public UserRole Role { get; }
    }

    internal enum UserRole
    {
        User,
        Admin,
    }
}
