using v.Base.Core.Domain;

namespace MyCom.Domain.Orders.ValueObjects;

public record OrderId : ValueObject
{
    public Guid Value { get; private set; }

    private OrderId() { }

    public static OrderId NewId()
    {
        return new() { Value = Guid.NewGuid() };
    }

    public static OrderId FromGuid(Guid id)
    {
        if (id == default)
        {
            throw new ArgumentException("The ID value must not be an empty id.");
        }

        return new() { Value = id };
    }
}
