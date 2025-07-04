using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Model
{
    public class Log
    {
        public Log(string change, DateTime dateTime)
        {
            Change = change;
            DateTime = dateTime;
        }

        public string Change { get; }
        public DateTime DateTime { get; }
    }
}
