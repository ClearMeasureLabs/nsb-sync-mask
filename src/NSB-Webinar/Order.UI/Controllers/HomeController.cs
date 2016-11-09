using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using NServiceBus;
using Order.Contracts;
using Order.UI.Models;

namespace Order.UI.Controllers
{
    public class HomeController : AsyncController
    {
        private readonly IBus _bus;

        public HomeController(IBus bus)
        {
            _bus = bus;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Order(string name)
        {
            await _bus.Send(new PlaceOrder { OrderId = Guid.NewGuid(), CustomerId = name })
                .Register(completion =>
                {
                    AsyncManager.Parameters["guid"] = ((PlaceOrderResponse) completion.Messages[0]).OrderId;
                });
            return Json(new OrderModel { Name = name, OrderId = Guid.Parse(AsyncManager.Parameters["guid"].ToString()) });
        }
    }
}