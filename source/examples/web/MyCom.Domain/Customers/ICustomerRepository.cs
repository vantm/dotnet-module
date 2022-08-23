using MyCom.Domain.Shared;

namespace MyCom.Domain.Customers;

public interface ICustomerRepository : IDatabaseRepository<Customer, long>
{
}
