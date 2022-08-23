using v.Base.Core.Domain;

namespace MyCom.Domain.Orders.ValueObjects;

public record BuyQuantity : ValueObject
{
    public int Value { get; private set; }

    public BuyQuantity Add(BuyQuantity qty)
    {
        return Create(Value + qty.Value);
    }

    public static BuyQuantity Create(int quantity)
    {
        if (quantity < 1)
        {
            throw new ArgumentException("The quantity must greater than zero.");
        }

        if (quantity > 100)
        {
            throw new ArgumentException("You order too much.");
        }

        return new() { Value = quantity };
    }
}
