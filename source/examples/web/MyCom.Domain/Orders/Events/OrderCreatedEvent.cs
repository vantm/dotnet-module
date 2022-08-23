using MyCom.Domain.Orders.ValueObjects;
using v.Base.Core.Domain;

namespace MyCom.Domain.Orders.Events;

public class OrderCreatedEvent : IDomainEvent
{
    public OrderId OrderId { get; }

    public OrderCreatedEvent(OrderId orderId)
    {
        OrderId = orderId;
    }
}
