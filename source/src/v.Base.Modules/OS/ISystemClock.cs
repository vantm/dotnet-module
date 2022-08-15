namespace v.Base.Modules.OS;

public interface ISystemClock
{
    DateOnly TodayUtc { get; }
    TimeOnly TimeNowUtc { get; }
}
