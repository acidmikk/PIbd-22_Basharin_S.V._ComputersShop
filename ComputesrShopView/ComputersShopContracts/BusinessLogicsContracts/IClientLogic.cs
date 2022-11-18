using ComputersShopContracts.BindingModels;
using ComputersShopContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace ComputersShopContracts.BusinessLogicsContracts
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}
