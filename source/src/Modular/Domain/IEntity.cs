namespace Modular.Domain;

public interface IEntity<out TId>
{
    TId Id { get; }
}
