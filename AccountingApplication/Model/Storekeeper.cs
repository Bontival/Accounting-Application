using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Model
{
    public class Storekeeper
    {
        public Storekeeper(string name, int id)
        {
            Name = name;
            this.Id = id;
        }
        public int Id { get; }
        public string Name { get; }
    }
}
