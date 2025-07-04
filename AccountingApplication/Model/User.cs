using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Model
{
    public class User
    {
        public User(string username, string password, int permissions, int? warehouse)
        {
            Username = username;
            Password = password;
            Permissions = permissions;
            WarehouseId = warehouse;
        }

        public string Username { get; }
        public string Password { get; }
        public int Permissions { get; }
        public int? WarehouseId {  get; }

    }
}
