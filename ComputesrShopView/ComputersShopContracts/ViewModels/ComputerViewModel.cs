using System.Collections.Generic;
using ComputersShopContracts.Attributes;
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
        [Column(title: "Название компьютера", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComputerName { get; set; }
        [DisplayName("Цена")]
        [Column(title: "Цена", gridViewAutoSize: GridViewAutoSize.Fill)]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ComputerComponents { get; set; }
    }
}
