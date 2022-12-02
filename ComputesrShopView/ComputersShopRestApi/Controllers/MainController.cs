using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicsContracts;
using ComputersShopContracts.ViewModels;
using ComputersShopDatabaseImplement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputersShopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IComputerLogic _computer;
        private readonly IMessageInfoLogic _messageInfoLogic;
        public MainController(IOrderLogic order, IComputerLogic computer, IMessageInfoLogic messageInfoLogic)
        {
            _order = order;
            _computer = computer;
            _messageInfoLogic = messageInfoLogic;
        }
        [HttpGet]
        public List<ComputerViewModel> GetComputerList() => _computer.Read(null)?.ToList();
        [HttpGet]
        public ComputerViewModel GetComputer(int computerId) => _computer.Read(new ComputerBindingModel { Id = computerId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);
        [HttpGet]
        public List<MessageInfoViewModel> GetMessage(int clientId) =>
          _messageInfoLogic.Read(new MessageInfoBindingModel
          {
              ClientId = clientId
          });
    }
}

