using AccountingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.ViewModel
{
    public class LogViewModel: ViewModelBase
    {
        public string Change { get; }
        public DateTime DateTime { get; }
        public LogViewModel(Log log)
        {
            Change = log.Change;
            DateTime = log.DateTime;    
        }
    }
}
