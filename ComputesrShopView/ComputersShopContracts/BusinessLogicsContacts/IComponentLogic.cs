using ComputersShopContracts.BindingModels;
using ComputersShopContracts.ViewModels;
using System.Collections.Generic;

namespace ComputersShopContracts.BusinessLogicsContacts
{
    public interface IComponentLogic
    {
        List<ComponentViewModel> Read(ComponentBindingModel model);
        void CreateOrUpdate(ComponentBindingModel model);
        void Delete(ComponentBindingModel model);
    }
}
