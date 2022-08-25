using ComputersShopContracts.BindingModels;
using ComputersShopContracts.StoragesContracts;
using ComputersShopContracts.ViewModels;
using ComputersShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputersShopListImplement.Implements
{
    public class ComputerStorage : IComputerStorage
    {
        private readonly DataListSingleton source;
        public ComputerStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ComputerViewModel> GetFullList()
        {
            var result = new List<ComputerViewModel>();
            foreach (var component in source.Computers)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<ComputerViewModel> GetFilteredList(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<ComputerViewModel>();
            foreach (var product in source.Computers)
            {
                if (product.ComputerName.Contains(model.ComputerName))
                {
                    result.Add(CreateModel(product));
                }
            }
            return result;
        }
        public ComputerViewModel GetElement(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var product in source.Computers)
            {
                if (product.Id == model.Id || product.ComputerName ==
                model.ComputerName)
                {
                    return CreateModel(product);
                }
            }
            return null;
        }
        public void Insert(ComputerBindingModel model)
        {
            var tempProduct = new Computer
            {
                Id = 1,
                ComputerComponents = new
            Dictionary<int, int>()
            };
            foreach (var product in source.Computers)
            {
                if (product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
            }
            source.Computers.Add(CreateModel(model, tempProduct));
        }
        public void Update(ComputerBindingModel model)
        {
            Computer tempProduct = null;
            foreach (var product in source.Computers)
            {
                if (product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (tempProduct == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempProduct);
        }
        public void Delete(ComputerBindingModel model)
        {
            for (int i = 0; i < source.Computers.Count; ++i)
            {
                if (source.Computers[i].Id == model.Id)
                {
                    source.Computers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Computer CreateModel(ComputerBindingModel model, Computer
        product)
        {
            product.ComputerName = model.ComputerName;
            product.Price = model.Price;
            // удаляем убранные
            foreach (var key in product.ComputerComponents.Keys.ToList())
            {
                if (!model.ComputerComponents.ContainsKey(key))
                {
                    product.ComputerComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.ComputerComponents)
            {
                if (product.ComputerComponents.ContainsKey(component.Key))
                {
                    product.ComputerComponents[component.Key] =
                    model.ComputerComponents[component.Key].Item2;
                }
                else
                {
                    product.ComputerComponents.Add(component.Key,
                    model.ComputerComponents[component.Key].Item2);
                }
            }
            return product;
        }
        private ComputerViewModel CreateModel(Computer product)
        {
            // требуется дополнительно получить список компонентов для изделия сназваниями и их количество
        var computerComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in product.ComputerComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                computerComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new ComputerViewModel
            {
                Id = product.Id,
                ComputerName = product.ComputerName,
                Price = product.Price,
                ComputerComponents = computerComponents
            };
        }
    }
}
