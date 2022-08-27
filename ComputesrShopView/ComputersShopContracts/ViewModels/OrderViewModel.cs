using System;
using System.ComponentModel;

namespace ComputersShopContracts.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ComputerId { get; set; }
        [DisplayName("Компьютер")]
        public string ComputerName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
