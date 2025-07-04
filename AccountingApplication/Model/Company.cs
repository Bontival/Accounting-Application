using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Model
{
    public class Company
    {
        private readonly List<MaterialCollection> _listOfMaterials = new List<MaterialCollection>();

        public Company(List<MaterialCollection> listOfMaterials)
        {
            _listOfMaterials = listOfMaterials;
        }
    }
}
