using System;
using NServiceBus;

namespace Order.Contracts
{
    public class PlaceOrderResponse : IMessage
    {
        public Guid OrderId { get; set; }
    }
}