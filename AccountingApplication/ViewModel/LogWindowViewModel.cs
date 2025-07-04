using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.ViewModel
{
    public class LogWindowViewModel : ViewModelBase
    {
        public ObservableCollection<LogViewModel> Logs { get; }

        public LogWindowViewModel(ObservableCollection<LogViewModel> logs)
        {
            Logs = logs;
        }

    }
}
