using v.Base.Core.Domain;

namespace MyCom.Domain.Customers;

public record CustomerId : ValueObject
{
    public long Value { get; private set; }

    private CustomerId() { }

    public static CustomerId Create(long id)
    {
        if (id < 1)
        {
            throw new ArgumentException("The id value must greater than zero.");
        }

        return new() { Value = id };
    }
}
