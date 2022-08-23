namespace DemoModule.Impl;

public class SystemClock : IClock
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
