using ComputersShopBusinessLogic.OfficePackage;
using ComputersShopBusinessLogic.OfficePackage.HelperModels;
using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicsContracts;
using ComputersShopContracts.StoragesContracts;
using ComputersShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputersShopBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IComputerStorage _productStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly ComputerSaveToExcel _saveToExcel;
        private readonly ComputerSaveToWord _saveToWord;
        private readonly ComputerSaveToPdf _saveToPdf;
        public ReportLogic(IComputerStorage productStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage,
        ComputerSaveToExcel saveToExcel, ComputerSaveToWord saveToWord,
       ComputerSaveToPdf saveToPdf)
        {
            _productStorage = productStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }
        public List<ReportComputerComponentViewModel> GetComputerComponent()
        {
            var components = _componentStorage.GetFullList();
            var products = _productStorage.GetFullList();
            var list = new List<ReportComputerComponentViewModel>();
            foreach (var component in components)
            {
                var record = new ReportComputerComponentViewModel
                {
                    ComponentName = component.ComponentName,
                    Computers = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var product in products)
                {
                    if (product.ComputerComponents.ContainsKey(component.Id))
                    {
                        record.Computers.Add(new Tuple<string, int>(product.ComputerName,
                       product.ComputerComponents[component.Id].Item2));
                        record.TotalCount +=
                       product.ComputerComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                ComputerName = x.ComputerName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
           .ToList();
        }
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Components = _componentStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveProductComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                ComputerComponents = GetComputerComponent()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
