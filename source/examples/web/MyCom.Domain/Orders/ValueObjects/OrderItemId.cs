using Modular.Domain;

namespace MyCom.Domain.Orders.ValueObjects;

public record OrderItemId : ValueObject
{
    public Guid Value { get; private set; }

    private OrderItemId() { }

    public static OrderItemId NewId()
    {
        return new() { Value = Guid.NewGuid() };
    }

    public static OrderItemId Create(Guid id)
    {
        if (id == default)
        {
            throw new ArgumentException("The ID value must not be an empty id.");
        }

        return new() { Value = id };
    }
}
