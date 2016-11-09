using System;
using System.Threading;
using NServiceBus;
using Order.Contracts;
using static System.Console;

namespace Order.Processor
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        private readonly IBus _bus;

        public PlaceOrderHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PlaceOrder message)
        {
            WriteLine($"Processing Order {message.OrderId} for customer {message.CustomerId}");
            Thread.Sleep(1000);
            WriteLine($"Order processed for {message.CustomerId}");
            _bus.Reply(new PlaceOrderResponse { OrderId = new Guid() });
        }
    }
}