using AccountingApplication.View;
using AccountingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountingApplication.Services
{
    public class WindowService: IWindowService
    {
        private ObservableCollection<LogViewModel> logs;

        public void OpenWindow()
        {
            var win = new LogWindow()
            {
                DataContext = new LogWindowViewModel(logs)
            };
            win.Show();
        }

        public WindowService(ObservableCollection<LogViewModel> logs)
        {
            this.logs = logs;
        }
    }
}
