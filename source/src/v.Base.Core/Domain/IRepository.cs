namespace v.Base.Core.Domain;

public interface IRepository
{
    Task<IEnumerable<IDomainEvent>> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<TAggregateRoot, TId> : IRepository where TAggregateRoot : IAggregateRoot<TId>
{
    Task<TAggregateRoot> FindAsync(TId id, CancellationToken cancellationToken = default);

    void RegisterAdd(TAggregateRoot model);

    void RegisterModified(TAggregateRoot model);

    void RegisterDeleted(TAggregateRoot model);
}
