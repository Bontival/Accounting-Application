using AccountingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.ViewModel
{
    public class StorekeeperViewModel: ViewModelBase
    {
        public int Id { get; }
        public string Name { get; }
        public StorekeeperViewModel(Storekeeper storekeeper)
        {
            Id = storekeeper.Id;
            Name = storekeeper.Name;
        }
        public override string ToString() { return Name; }
    }
}
