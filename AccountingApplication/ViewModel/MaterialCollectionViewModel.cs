using AccountingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.ViewModel
{
    public class MaterialCollectionViewModel : ViewModelBase
    {
        public string Material { get; private set; }
        public string Storekeeper { get; private set; }
        public string Warehouse { get; private set; }
        public int Amount { get; private set; }
        public MaterialCollectionViewModel(MaterialCollection materialCollection)
        {
            Material = materialCollection.Material.MaterialName;
            Storekeeper = materialCollection.Storekeeper.Name;
            Warehouse = materialCollection.Warehouse.Name;
            Amount = materialCollection.Amount;
        }
        public void changeElement(int amount, string s)
        {
            Amount = amount;
            Storekeeper = s;
        }
    }
}
