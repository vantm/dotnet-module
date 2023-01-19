namespace Modular.Models;

public class PageResult<T>
{
    protected long _total;
    protected T[] _items = Array.Empty<T>();

    public long Total { get => _total; init => _total = value; }
    public T[] Items { get => _items; init => _items = value; }
}

public class PageResult<T, TParams> : PageResult<T>, IPageParams where TParams : PageParams
{
    public long Offset { get; private set; }

    public int Limit { get; private set; }

    public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

    public void SetParams(TParams? pageParams)
    {
        if (pageParams == null)
        {
            return;
        }

        Offset = pageParams.Offset;
        Limit = pageParams.Limit;

        SetMoreParams(pageParams);
    }

    protected virtual void SetMoreParams(TParams pageParams)
    {
    }

    public void SetResult(IEnumerable<T> items, long total)
    {
        if (items != null)
        {
            _items = items.ToArray();
        }

        _total = total;
    }
}
