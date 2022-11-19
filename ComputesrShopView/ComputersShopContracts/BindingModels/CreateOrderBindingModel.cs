using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    public class CreateOrderBindingModel
    {
        public int ComputerId { get; set; }
        public int ClientId { get; set; }
        public int Count { get; set; }
        public int Sum { get; set; }
    }
}
