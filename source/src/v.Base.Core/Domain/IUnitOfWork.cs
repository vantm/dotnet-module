namespace v.Base.Core.Domain;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}
