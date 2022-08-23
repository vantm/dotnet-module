using MyCom.Domain.Shared;

namespace MyCom.Domain.Carts;

public interface ICartRepository : IDatabaseRepository<Cart, long>
{
}
