using AccountingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.ViewModel
{
    public class WarehouseViewModel: ViewModelBase
    {
        public int Id { get; }
        public string Name { get; }
        public WarehouseViewModel(Warehouse warehouse)
        {
            Id = warehouse.Id;
            Name = warehouse.Name;
        }
        public override string ToString() { return Name; }
    }
}
