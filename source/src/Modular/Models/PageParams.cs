namespace Modular.Models;

public class PageParams : IPageParams
{
    public long Offset { get; init; } = 1;
    public int Limit { get; init; } = 50;
}

