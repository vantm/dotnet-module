namespace DemoModule.Impl;

public class DefaultRandomUtil : IRandomUtil
{
    private readonly Random _rnd;

    public DefaultRandomUtil()
    {
        _rnd = new((int)DateTime.Now.Ticks);
    }

    public int Next(int max)
    {
        return _rnd.Next(max);
    }

    public int Next(int min, int max)
    {
        return _rnd.Next(min, max);
    }
}
