using System.Linq.Expressions;

namespace MyCom.Domain.Shared;

public interface IDatabaseRepository<T, TId> where T : class
{
    Task<T?> FindAsync(TId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> SelectAsync(SelectParams<T> @params, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> InsertAsync(T product, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(T product, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}
