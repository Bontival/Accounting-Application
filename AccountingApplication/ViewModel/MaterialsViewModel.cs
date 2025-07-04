using AccountingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.ViewModel
{
    public class MaterialsViewModel: ViewModelBase
    {
        public int Id { get; }
        public string Name { get; }
        public MaterialsViewModel(Material materials)
        {
            Id = materials.Id;
            Name = materials.MaterialName;
        }
        public override string ToString() { return Name; }
    }
}
