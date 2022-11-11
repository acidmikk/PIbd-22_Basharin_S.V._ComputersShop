using ComputersShopContracts.ViewModels;
using System.Collections.Generic;

namespace ComputersShopBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportComputerComponentViewModel> ComputerComponents { get; set; }
    }
}
