using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Model
{
    public class MaterialCollection
    {
        public Material Material { get; }
        public Storekeeper Storekeeper { get; }
        public Warehouse Warehouse { get; }
        public int Amount { get; }

        public MaterialCollection(Material material, Storekeeper storekeeper, Warehouse warehouse, int amount)
        {
            Material = material;
            Storekeeper = storekeeper;
            Warehouse = warehouse;
            Amount = amount;
        }
    }
}
