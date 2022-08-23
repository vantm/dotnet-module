using v.Base.Core.Domain;

namespace MyCom.Domain.Products;

public record ProductId : ValueObject
{
    public long Value { get; private set; }

    private ProductId() { }

    public static ProductId Create(long id)
    {
        if (id < 1)
        {
            throw new ArgumentException("The id value must greater than zero.");
        }

        return new() { Value = id };
    }
}
