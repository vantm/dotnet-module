namespace Modular.Domain;

public interface IAggregateRoot<out TId> : IEntity<TId>
{
}
