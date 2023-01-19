namespace Modular.Models;

public interface IPageParams
{
    int Limit { get; }
    long Offset { get; }
}
