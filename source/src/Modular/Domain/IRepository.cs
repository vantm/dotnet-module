namespace Modular.Domain;

public interface IRepository<TAggregateRoot, TId> where TAggregateRoot : IAggregateRoot<TId>
{
    Task<TAggregateRoot> FindAsync(TId id, CancellationToken cancellationToken = default);

    void RegisterAdd(TAggregateRoot model);

    void RegisterModified(TAggregateRoot model);

    void RegisterDeleted(TAggregateRoot model);
}
