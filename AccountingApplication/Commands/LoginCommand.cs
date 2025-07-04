using AccountingApplication.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly LoginViewModel _loginViewModel;
        private bool _connectionClosed;
        public LoginCommand(LoginViewModel loginViewModel, MainWindowViewModel mainWindowViewModel)
        {
            _connectionClosed = true;
            _mainWindowViewModel = mainWindowViewModel;
            _loginViewModel = loginViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_connectionClosed)
            {
                if (_mainWindowViewModel.OpenConnection(_loginViewModel.Ip))
                {
                    _mainWindowViewModel.FillViewModels();
                    _connectionClosed = false;
                }
                else
                {
                    _loginViewModel.Message = "Не удалось подключиться к серверу";
                    return;
                }
            }
            UserViewModel tempUsername = _mainWindowViewModel.Users.FirstOrDefault(i => i.Username == _loginViewModel.Username);
            if (tempUsername != null)
            {
                if(tempUsername.Password == _loginViewModel.Password)
                {
                    _mainWindowViewModel.ValidateLogin(tempUsername);
                    _loginViewModel.CloseWindow();
                    return;
                }            
            }
            _loginViewModel.Message = "Неверный логин или пароль";
        }
    }
}
