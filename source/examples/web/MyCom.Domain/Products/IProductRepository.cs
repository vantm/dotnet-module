using MyCom.Domain.Shared;

namespace MyCom.Domain.Products;

public interface IProductRepository : IDatabaseRepository<Product, long>
{
}

