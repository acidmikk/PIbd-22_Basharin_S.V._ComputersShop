using System.Collections.Generic;

namespace ComputersShopListImplement.Models
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Computer
    {
        public int Id { get; set; }
        public string ComputerName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> ComputerComponents { get; set; }
    }
}

