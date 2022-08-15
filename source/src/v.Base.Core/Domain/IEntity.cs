namespace v.Base.Core.Domain;

public interface IEntity<out TId>
{
    TId Id { get; }
    bool IsTransient { get; }
}
