using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AccountingApplication.Model;

namespace AccountingApplication.ViewModel
{
    public class MaterialTableViewModel : ViewModelBase
    {
        private ObservableCollection<MaterialCollectionViewModel> materialCollectionViewModels = new ObservableCollection<MaterialCollectionViewModel>();
        public IEnumerable<MaterialCollectionViewModel> MaterialCollection => materialCollectionViewModels;
        private MaterialCollectionViewModel selectedItem;
        private MainWindowViewModel mainWindowViewModel;
        private List<MaterialCollectionViewModel> materialCollectionViewModels1;

        public MaterialCollectionViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                if (selectedItem != null) 
                {
                    mainWindowViewModel.SelectedWarehouse = selectedItem.Warehouse;
                    mainWindowViewModel.SelectedMaterial = selectedItem.Material;
                    mainWindowViewModel.SelectedStorekeeper = selectedItem.Storekeeper;
                }
            }
        }
        public MaterialTableViewModel(MainWindowViewModel mainWindowViewModel, List<MaterialCollectionViewModel> materialList)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            if (materialList.Count != 0)
            {
                foreach (MaterialCollectionViewModel material_element in materialList)
                {
                    materialCollectionViewModels.Add(material_element);
                }
            }
        }
        public MaterialTableViewModel()
        {
            /*foreach (DataRow row in dt.Rows)
            {
                materialCollectionViewModels.Add(new MaterialCollectionViewModel(new MaterialCollection(
                    new Material(row.Field<string>("material_name")),
                    new Storekeeper(row.Field<string>("storekeeper")),
                    new Warehouse(row.Field<string>("warehouse")), 
                    row.Field<int>("amount")
                    )));
            }*/
        }

        public void UpdateMaterialListTable(IEnumerable<MaterialCollectionViewModel> enumerable, UserViewModel curUser, ObservableCollection<WarehouseViewModel> warehouses)
        {
            materialCollectionViewModels.Clear();
            if (curUser.Permissions == 1)
            {
                foreach (MaterialCollectionViewModel material_element in enumerable.ToList())
                {
                    var tempWarehouseId = warehouses.FirstOrDefault(i => i.Name == material_element.Warehouse).Id;
                    if (tempWarehouseId == curUser.WarehouseId)
                    {
                        materialCollectionViewModels.Add(material_element);
                    }
                }
            }
            else if (curUser.Permissions > 1)
            {
                foreach (MaterialCollectionViewModel material_element in enumerable.ToList())
                {
                    materialCollectionViewModels.Add(material_element);
                }
            }
        }
        public void UpdateOrAddElement(MaterialCollectionViewModel materialCollectionViewModel)
        {

            materialCollectionViewModels.Add(materialCollectionViewModel);
        }
    }
}
