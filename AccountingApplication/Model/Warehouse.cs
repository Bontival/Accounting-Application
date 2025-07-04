using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Model
{
    public class Warehouse
    {
        public Warehouse(string name, int id)
        {
            this.Id = id;
            Name = name;
        }
        public int Id { get; }
        public string Name { get; }
    }
}
