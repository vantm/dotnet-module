using MyCom.Domain.Orders.ValueObjects;
using MyCom.Domain.Products;
using v.Base.Core.Domain;

namespace MyCom.Domain.Orders.Events;

public class OrderItemAddedEvent : IDomainEvent
{
    public OrderId OrderId { get; }
    public ProductId ProductId { get; }

    public OrderItemAddedEvent(OrderId orderId, ProductId productId)
    {
        OrderId = orderId;
        ProductId = productId;
    }
}
