namespace v.Base.Modules.OS;

public static class SystemClockExtensions
{
    public static DateTimeOffset GetNowAtUtc(this ISystemClock clock)
    {
        var today = clock.TodayUtc;
        var now = clock.TimeNowUtc;
        return new(today.Year, today.Month, today.Day, now.Hour, now.Minute, now.Second, now.Millisecond, now.Microsecond, TimeSpan.Zero);
    }

    public static DateTimeOffset GetNowAt(this ISystemClock clock, TimeSpan tzOffset)
    {
        var now = clock.GetNowAtUtc();
        return now.ToOffset(tzOffset);
    }
}
