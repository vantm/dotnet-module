namespace MyCom.Domain.Carts;

public class Cart
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public string? Items { get; set; }
}
