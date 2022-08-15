namespace v.Base.Core.Models;

public interface IPageParams
{
    int Limit { get; }
    long Offset { get; }
}
