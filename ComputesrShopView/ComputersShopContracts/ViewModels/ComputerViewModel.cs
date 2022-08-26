using System.Collections.Generic;
using System.ComponentModel;

namespace ComputersShopContracts.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class ComputerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компьютера")]
        public string ComputerName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ComputerComponents { get; set; }
    }
}
