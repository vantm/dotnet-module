namespace v.Base.Modules.OS.Impl;

public class DefaultSystemClock : ISystemClock
{
    public DateOnly TodayUtc
    {
        get
        {
            var now = DateTime.UtcNow;
            return new(now.Year, now.Month, now.Day);
        }
    }

    public TimeOnly TimeNowUtc
    {
        get
        {
            var now = DateTime.Now;
            return new(now.Hour, now.Minute, now.Second, now.Millisecond, now.Microsecond);
        }
    }
}
