using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputersShopContracts.BindingModels;
using ComputersShopContracts.StoragesContracts;
using ComputersShopContracts.ViewModels;
using ComputersShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputersShopDatabaseImplement.Implements
{
    public class ComputerStorage : IComputerStorage
    {
        public List<ComputerViewModel> GetFullList()
        {
            using var context = new ComputerShopDatabase();
            return context.Computers
            .Include(rec => rec.ComputerComponents)
            .ThenInclude(rec => rec.Component)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<ComputerViewModel> GetFilteredList(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Computers
            .Include(rec => rec.ComputerComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.ComputerName.Contains(model.ComputerName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public ComputerViewModel GetElement(ComputerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            var car = context.Computers
            .Include(rec => rec.ComputerComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.ComputerName == model.ComputerName || rec.Id == model.Id);
            return car != null ? CreateModel(car) : null;
        }
        public void Insert(ComputerBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Computer computer = new Computer()
                {
                    ComputerName = model.ComputerName,
                    Price = model.Price
                };
                context.Computers.Add(computer);
                context.SaveChanges();
                CreateModel(model, computer, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(ComputerBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Computers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(ComputerBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Computer element = context.Computers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Computers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Computer CreateModel(ComputerBindingModel model, Computer computer, ComputerShopDatabase context)
        {
            computer.ComputerName = model.ComputerName;
            computer.Price = model.Price;
            if (model.Id.HasValue)
            {
                var computerComponent = context.ComputerComponents.Where(rec =>
               rec.ComputerId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ComputerComponents.RemoveRange(computerComponent.Where(rec =>
               !model.ComputerComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateDetail in computerComponent)
                {
                    updateDetail.Count =
                    model.ComputerComponents[updateDetail.ComponentId].Item2;
                    model.ComputerComponents.Remove(updateDetail.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.ComputerComponents)
            {
                context.ComputerComponents.Add(new ComputerComponent
                {
                    ComputerId = computer.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return computer;
        }
        private static ComputerViewModel CreateModel(Computer computer)
        {
            return new ComputerViewModel
            {
                Id = computer.Id,
                ComputerName = computer.ComputerName,
                Price = computer.Price,
                ComputerComponents = computer.ComputerComponents
            .ToDictionary(recPC => recPC.ComponentId,
            recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }
}
