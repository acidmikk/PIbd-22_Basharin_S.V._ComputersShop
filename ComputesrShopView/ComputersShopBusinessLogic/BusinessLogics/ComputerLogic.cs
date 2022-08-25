using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicsContracts;
using ComputersShopContracts.StoragesContracts;
using ComputersShopContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace ComputersShopBusinessLogic.BusinessLogics
{
    public class ComputerLogic : IComputerLogic
    {
        private readonly IComputerStorage _computerStorage;
        public ComputerLogic(IComputerStorage computerStorage)
        {
            _computerStorage = computerStorage;
        }
        public List<ComputerViewModel> Read(ComputerBindingModel model)
        {
            if (model == null)
            {
                return _computerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ComputerViewModel> { _computerStorage.GetElement(model) };
            }
            return _computerStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ComputerBindingModel model)
        {
            var element = _computerStorage.GetElement(new ComputerBindingModel
            {
                ComputerName = model.ComputerName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компютер с таким названием");
            }
            if (model.Id.HasValue)
            {
                _computerStorage.Update(model);
            }
            else
            {
                _computerStorage.Insert(model);
            }
        }
        public void Delete(ComputerBindingModel model)
        {
            var element = _computerStorage.GetElement(new ComputerBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Компьютер не найден");
            }
            _computerStorage.Delete(model);
        }
    }
}