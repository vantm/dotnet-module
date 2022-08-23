#nullable disable

using MyCom.Domain.Customers;
using MyCom.Domain.Orders.Events;
using MyCom.Domain.Orders.ValueObjects;
using MyCom.Domain.Products;
using v.Base.Core.Domain;

namespace MyCom.Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();

    private Order(OrderId id) : base(id)
    {
    }

    public CustomerId CustomerId { get; private set; }

    public IEnumerable<OrderItem> Items { get => _items.AsReadOnly(); }

    public void AddItem(ProductId productId, BuyQuantity quantity)
    {
        _items.Add(OrderItem.Create(productId, quantity));
        AddEvent(new OrderItemAddedEvent(Id, productId));
    }

    public void RemoveItem(ProductId productId)
    {
        _items.RemoveAll(x => x.ProductId == productId);
    }

    public static Order CreateNew(IEnumerable<OrderItem> items)
    {
        Order order = new(OrderId.NewId());
        order._items.AddRange(items);
        order.AddEvent(new OrderCreatedEvent(order.Id));
        return order;
    }
}
