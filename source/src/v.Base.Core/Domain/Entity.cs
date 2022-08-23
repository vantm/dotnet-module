namespace v.Base.Core.Domain;

public abstract class Entity<T> : IEntity<T> where T : ValueObject
{
    public T Id { get; }

    protected Entity(T id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        return ((IEntity<T>)obj).Id == Id;
    }

    protected static int RequestHashCode = 16;

    public override int GetHashCode()
    {
        return Id.GetHashCode() ^ RequestHashCode;
    }
}
