using ComputersShopContracts.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ComputersShopContracts.ViewModels
{
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }
        public int ComputerId { get; set; }
        public int ClientId { get; set; }
        [DisplayName("ФИО исполнителя")]
        [Column(title: "Исполнитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string? ImplementerFullName { get; set; }
        [DisplayName("ФИО Клиента")]
        [Column(title: "Клиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFullName { get; set; }
        [DisplayName("Компьютер")]
        [Column(title: "Компьютер", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComputerName { get; set; }
        [DisplayName("Количество")]
        [Column(title: "Количество", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        [Column(title: "Сумма", gridViewAutoSize: GridViewAutoSize.Fill)]
        public int Sum { get; set; }
        [DisplayName("Статус")]
        [Column(title: "Статус", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Status { get; set; }
        [DisplayName("Дата создания")]
        [Column(title: "Дата создания", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        [Column(title: "Дата выполнения", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime? DateImplement { get; set; }
        public int? ImplementerId { get; set; }
    }
}
