using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersShopDatabaseImplement.Models
{
    public class Computer
    {
        public int Id { get; set; }
        [Required]
        public string ComputerName { get; set; }
        [Required]
        public int Price { get; set; }

        [ForeignKey("ComputerId")]
        public virtual List<ComputerComponent> ComputerComponents { get; set; }

        [ForeignKey("ComputerId")]
        public virtual List<Order> Order { get; set; }
    }
}
