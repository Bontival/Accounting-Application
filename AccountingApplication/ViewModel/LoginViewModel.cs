using AccountingApplication.Commands;
using AccountingApplication.Model;
using Mysqlx.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountingApplication.ViewModel
{
    public class LoginViewModel : ViewModelBase, ICloseWindow
    {
        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string ip;
        public string Ip
        {
            get { return ip; }
            set
            {
                ip = value;
                OnPropertyChanged(nameof(Ip));
            }
        }
        public ICommand LoginCommand { get; }
        public LoginViewModel(MainWindowViewModel mainWindowViewModel)
        {
            message = "";
            ip = "localhost";
            LoginCommand = new LoginCommand(this, mainWindowViewModel);
        }
        public void CloseWindow()
        {
            Close?.Invoke();
        }
        public Action Close { get; set; }
    }
    interface ICloseWindow
    {
        Action Close { get; set; }
    }
}
