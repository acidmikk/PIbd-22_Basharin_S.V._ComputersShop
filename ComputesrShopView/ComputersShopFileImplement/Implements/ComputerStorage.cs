using ComputersShopContracts.BindingModels;
using ComputersShopContracts.StoragesContracts;
using ComputersShopContracts.ViewModels;
using ComputersShopFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ComputersShopFileImplement.Implements
{
    public class ComputerStorage : IComputerStorage
    {
        private readonly FileDataListSingleton source;
        public ComputerStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<ComputerViewModel> GetFullList()
        {
            return source.Computers
            .Select(CreateModel)
            .ToList();
        }
        public List<ComputerViewModel> GetFilteredList(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Computers
            .Where(rec => rec.ComputerName.Contains(model.ComputerName))
            .Select(CreateModel)
            .ToList();
        }
        public ComputerViewModel GetElement(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var product = source.Computers
            .FirstOrDefault(rec => rec.ComputerName == model.ComputerName || rec.Id
           == model.Id);
            return product != null ? CreateModel(product) : null;
        }
        public void Insert(ComputerBindingModel model)
        {
            int maxId = source.Computers.Count > 0 ? source.Components.Max(rec => rec.Id)
: 0;
            var element = new Computer
            {
                Id = maxId + 1,
                ComputerComponents = new
           Dictionary<int, int>()
            };
            source.Computers.Add(CreateModel(model, element));
        }
        public void Update(ComputerBindingModel model)
        {
            var element = source.Computers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(ComputerBindingModel model)
        {
            Computer element = source.Computers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Computers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Computer CreateModel(ComputerBindingModel model, Computer
        computer)
        {
            computer.ComputerName = model.ComputerName;
            computer.Price = model.Price;
            // удаляем убранные
            foreach (var key in computer.ComputerComponents.Keys.ToList())
            {
                if (!model.ComputerComponents.ContainsKey(key))
                {
                    computer.ComputerComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.ComputerComponents)
            {
                if (computer.ComputerComponents.ContainsKey(component.Key))
                {
                    computer.ComputerComponents[component.Key] =
                    model.ComputerComponents[component.Key].Item2;
                }
                else
                {
                    computer.ComputerComponents.Add(component.Key,
                    model.ComputerComponents[component.Key].Item2);
                }
            }
            return computer;
        }
        private ComputerViewModel CreateModel(Computer product)
        {
            // требуется дополнительно получить список компонентов для изделия сназваниями и их количество
            return new ComputerViewModel
            {
                Id = product.Id,
                ComputerName = product.ComputerName,
                Price = product.Price,
                ComputerComponents = product.ComputerComponents.ToDictionary(recPC => recPC.Key, recPC =>
                (source.Components.FirstOrDefault(recC => recC.Id ==
                recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}
