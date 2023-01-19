using MyCom.Domain.Orders.ValueObjects;
using Modular.Domain;

namespace MyCom.Domain.Orders.Events;

public class OrderCreatedEvent : IDomainEvent
{
    public OrderId OrderId { get; }

    public OrderCreatedEvent(OrderId orderId)
    {
        OrderId = orderId;
    }
}
