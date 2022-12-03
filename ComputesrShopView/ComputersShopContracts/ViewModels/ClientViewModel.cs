using System.ComponentModel;
using ComputersShopContracts.Attributes;

namespace ComputersShopContracts.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО клиента")]
        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FullName { get; set; }
        [DisplayName("Логин клиента")]
        [Column(title: "Логин клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Login { get; set; }
        [DisplayName("Пароль клиента")]
        [Column(title: "Пароль клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Password { get; set; }
    }
}
