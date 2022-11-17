﻿using System;
using ComputersShopContracts.Enums;

namespace ComputersShopContracts.BindingModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public int ComputerId { get; set; }
        public int Count { get; set; }
        public int Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

}
