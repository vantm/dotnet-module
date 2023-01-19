#nullable disable

using MyCom.Domain.Orders.ValueObjects;
using MyCom.Domain.Products;
using Modular.Domain;

namespace MyCom.Domain.Orders
{
    public class OrderItem : Entity<OrderItemId>
    {
        private OrderItem(OrderItemId id) : base(id)
        {
        }

        public ProductId ProductId { get; private set; }

        public BuyQuantity Quantity { get; private set; }

        public static OrderItem Create(ProductId productId, BuyQuantity quantity)
        {
            return new(OrderItemId.NewId())
            {
                ProductId = productId,
                Quantity = quantity
            };
        }
    }
}
