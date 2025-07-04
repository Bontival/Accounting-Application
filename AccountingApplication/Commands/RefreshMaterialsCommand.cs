using AccountingApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApplication.Commands
{
    public class RefreshMaterialsCommand : CommandBase
    {
        private MainWindowViewModel mainWindowViewModel;

        public RefreshMaterialsCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public override void Execute(object parameter)
        {
            for (int i = 0; i < mainWindowViewModel.Materials.Count; i++)
            {
                bool notInList = true;
                for (int j = 0; j < mainWindowViewModel.MaterialList.Count; j++)
                {
                    if (mainWindowViewModel.Materials[i].Name == mainWindowViewModel.MaterialList[j].Material)
                    {
                        notInList = false;
                    }
                }
                if (notInList)
                {
                    mainWindowViewModel.DeleteMaterialFromDb(mainWindowViewModel.Materials[i]);
                    mainWindowViewModel.Materials.RemoveAt(i);
                }
            }
        }
    }
}
/*foreach (var materialList in mainWindowViewModel.MaterialList)
{
    if (material.Name != materialList.Material)
    {
        material.
                    }
}*/