using ComputersShopContracts.BindingModels;
using ComputersShopContracts.ViewModels;
using System.Collections.Generic;


namespace ComputersShopContracts.BusinessLogicsContracts
{
    public interface IComputerLogic
    {
        List<ComputerViewModel> Read(ComputerBindingModel model);
        void CreateOrUpdate(ComputerBindingModel model);
        void Delete(ComputerBindingModel model);
    }
}
