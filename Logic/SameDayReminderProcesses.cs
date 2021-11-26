namespace Reminders.CoreLibrary.Logic;
public class SameDayReminderProcesses : BasicSubReminderProcesses
{
    private readonly ISameDayReminderDataAccess _data;
    public SameDayReminderProcesses(ISameDayReminderDataAccess data, ISnoozeDataAccess snoozeData) : base(data, snoozeData)
    {
        _data = data;
    }
    public override Task<ReminderModel?> GetNextReminderAsync()
    {
        return _data.GetNextReminderAsync();
    }
}