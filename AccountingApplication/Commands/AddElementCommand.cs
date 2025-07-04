using AccountingApplication.Model;
using AccountingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Commands
{
    public class AddElementCommand : CommandBase
    {
        private MaterialsViewModel _tempMaterial;
        MainWindowViewModel _mainWindowViewModel;

        public AddElementCommand(MainWindowViewModel mainWindowViewModel) 
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        public override void Execute(object parameter)
        {
            var w = _mainWindowViewModel.SelectedWarehouse;
            var m = _mainWindowViewModel.SelectedMaterial;
            var a = _mainWindowViewModel.SelectedAmount;
            var s = _mainWindowViewModel.SelectedStorekeeper;
            if (w != null && m != null && a != null && s != null)
            {
                _tempMaterial = _mainWindowViewModel.Materials.FirstOrDefault(i => i.Name == m);
                if (_tempMaterial != null)
                {
                    var tempMaterialCollection = _mainWindowViewModel.MaterialList.FirstOrDefault(i => i.Material == _tempMaterial.Name && i.Warehouse == w);//
                    if (tempMaterialCollection != null)
                    {
                        var newAmount = tempMaterialCollection.Amount + int.Parse(a);
                        tempMaterialCollection.changeElement(newAmount, s);
                        _mainWindowViewModel.CurrentViewModel.UpdateMaterialListTable(_mainWindowViewModel.MaterialList, _mainWindowViewModel.CurrentUser, _mainWindowViewModel.Warehouses);
                        _mainWindowViewModel.UpdateMaterialListDb(tempMaterialCollection);
                    }
                    else
                    {
                        var tempStorekeeper = _mainWindowViewModel.Storekeepers.FirstOrDefault(i => i.Name == s);
                        var tempWarehouse = _mainWindowViewModel.Warehouses.FirstOrDefault(i => i.Name == w);
                        var newMaterialCollection = new MaterialCollectionViewModel(new MaterialCollection(
                            new Material(_tempMaterial.Name, _tempMaterial.Id),
                            new Storekeeper(tempStorekeeper.Name, tempStorekeeper.Id),
                            new Warehouse(tempWarehouse.Name, tempWarehouse.Id),
                            int.Parse(a)
                            ));
                        _mainWindowViewModel.MaterialList.Add(newMaterialCollection);
                        _mainWindowViewModel.CurrentViewModel.UpdateMaterialListTable(_mainWindowViewModel.MaterialList, _mainWindowViewModel.CurrentUser, _mainWindowViewModel.Warehouses);
                        _mainWindowViewModel.AddMaterialListToDb(_tempMaterial, tempStorekeeper, tempWarehouse, int.Parse(a));
                    }
                }
                else
                {
                    var newMaterialId = _mainWindowViewModel.AddMaterialToDb(m);
                    var newMaterial = new MaterialsViewModel(new Material(m, newMaterialId));
                    _mainWindowViewModel.Materials.Add(newMaterial);
                    var tempStorekeeper = _mainWindowViewModel.Storekeepers.FirstOrDefault(i => i.Name == s);
                    var tempWarehouse = _mainWindowViewModel.Warehouses.FirstOrDefault(i => i.Name == w);
                    _mainWindowViewModel.MaterialList.Add(new MaterialCollectionViewModel(new MaterialCollection(
                        new Material(newMaterial.Name, newMaterial.Id),
                        new Storekeeper(tempStorekeeper.Name, tempStorekeeper.Id),
                        new Warehouse(tempWarehouse.Name, tempWarehouse.Id),
                        int.Parse(a)
                        )));
                    _mainWindowViewModel.CurrentViewModel.UpdateMaterialListTable(_mainWindowViewModel.MaterialList, _mainWindowViewModel.CurrentUser, _mainWindowViewModel.Warehouses);
                    _mainWindowViewModel.AddMaterialListToDb(newMaterial, tempStorekeeper, tempWarehouse, int.Parse(a));
                }
            }
        }
    }
}
