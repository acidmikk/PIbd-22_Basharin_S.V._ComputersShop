using ComputersShopContracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }
        [DisplayName("Отправитель")]
        [Column(title: "Отправитель", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SenderName { get; set; }
        [DisplayName("Дата письма")]
        [Column(title: "Дата письма", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime DateDelivery { get; set; }
        [DisplayName("Заголовок")]
        [Column(title: "Заголовок", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Subject { get; set; }
        [DisplayName("Текст")]
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }
}
