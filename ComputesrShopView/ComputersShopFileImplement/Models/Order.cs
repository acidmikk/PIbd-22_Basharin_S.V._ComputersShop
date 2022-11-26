using ComputersShopContracts.Enums;
using System;

namespace ComputersShopFileImplement.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public int ComputerId { get; set; }
        public int ClientId { get; set; }
        public int? ImplementerId { get; set; }
        public int Count { get; set; }
        public int Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }

}
