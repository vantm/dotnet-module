namespace v.Base.Core.Domain;

public interface IAggregateRoot<out TId> : IEntity<TId>
{
}
