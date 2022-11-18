using System;

namespace ComputersShopContracts.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public string ComputerName { get; set; }
        public int Count { get; set; }
        public int Sum { get; set; }
        public string Status { get; set; }
    }
}
