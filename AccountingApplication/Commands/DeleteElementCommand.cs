using AccountingApplication.Model;
using AccountingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Commands
{
    public class DeleteElementCommand : CommandBase
    {
        private MaterialsViewModel _tempMaterial;
        MainWindowViewModel _mainWindowViewModel;

        public DeleteElementCommand(MainWindowViewModel mainWindowViewModel)
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
                    var tempMaterialCollection = _mainWindowViewModel.MaterialList.FirstOrDefault(i => i.Material == _tempMaterial.Name && i.Warehouse == w);
                    if (tempMaterialCollection != null)
                    {
                        var newAmount = tempMaterialCollection.Amount - int.Parse(a);
                        if (newAmount < 1) 
                        {
                            _mainWindowViewModel.MaterialList.Remove(tempMaterialCollection);
                            _mainWindowViewModel.DeleteElementFromDb(tempMaterialCollection);
                        }
                        else
                        {
                            tempMaterialCollection.changeElement(newAmount, s);
                            _mainWindowViewModel.UpdateMaterialListDb(tempMaterialCollection);
                        }
                        _mainWindowViewModel.CurrentViewModel.UpdateMaterialListTable(_mainWindowViewModel.MaterialList, _mainWindowViewModel.CurrentUser, _mainWindowViewModel.Warehouses);
                    }
                }
            }
        }
    }
}
