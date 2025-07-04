using AccountingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.ViewModel
{
    public class UserViewModel: ViewModelBase
    {
        public UserViewModel(User user)
        {
            Username = user.Username;
            Password = user.Password;
            Permissions = user.Permissions;
            WarehouseId = user.WarehouseId;
        }
        public string Username { get; }
        public string Password { get; }
        public int Permissions { get; }
        public int? WarehouseId { get; }
    }
}
