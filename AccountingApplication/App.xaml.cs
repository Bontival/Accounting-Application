using AccountingApplication.View;
using AccountingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AccountingApplication
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel mainWindowViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindowViewModel = new MainWindowViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = mainWindowViewModel
            };
            LoginView loginWindow = new LoginView
            {
                DataContext = new LoginViewModel(mainWindowViewModel),
                Topmost = true
            };
            MainWindow.Show();
            loginWindow.ShowDialog();


            base.OnStartup(e);
        }
    }
}
