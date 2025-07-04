using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Model
{
    public class Material
    {
        public Material(string MaterialName, int id)
        {
            this.Id = id;
            this.MaterialName = MaterialName;
        }
        public int Id { get; }
        public string MaterialName { get; }
    }
}
