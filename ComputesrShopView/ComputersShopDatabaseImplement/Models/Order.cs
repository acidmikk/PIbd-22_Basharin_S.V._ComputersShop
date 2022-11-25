using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputersShopContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ComputersShopDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ComputerId { get; set; }
        public int ClientId { get; set; }
        public int? ImplementerId { get; set; }

        [Required]
        public int Count { get; set; }
        [Required]
        public int Sum { get; set; }
        [Required]
        public OrderStatus? Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Computer Computer { get; set; }
        public virtual Client Client { get; set; }
        public virtual Implementer Implementer { get; set; }
    }
}
