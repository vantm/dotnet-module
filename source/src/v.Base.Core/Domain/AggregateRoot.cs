namespace v.Base.Core.Domain;

public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T> where T : ValueObject
{
    static AggregateRoot()
    {
        RequestHashCode = 32;
    }

    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(T id) : base(id)
    {
    }

    protected void AddEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
