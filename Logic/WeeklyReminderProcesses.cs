namespace Reminders.CoreLibrary.Logic;
public class WeeklyReminderProcesses : BasicSubReminderProcesses
{
    private readonly ISimpleWeeklyDataAccess _data;
    public WeeklyReminderProcesses(ISimpleWeeklyDataAccess data, ISnoozeDataAccess snoozeData) : base(data, snoozeData)
    {
        _data = data;
    }
    public override Task<ReminderModel?> GetNextReminderAsync()
    {
        return _data.GetNextWeeklyReminderAsync()!;
    }
}