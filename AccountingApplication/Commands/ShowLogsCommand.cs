using AccountingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Commands
{
    public class ShowLogsCommand : CommandBase
    {
        private MainWindowViewModel mainWindowViewModel;
        public ShowLogsCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }
        public override void Execute(object parameter)
        {
            mainWindowViewModel.WindowService.OpenWindow();
        }
    }
}
