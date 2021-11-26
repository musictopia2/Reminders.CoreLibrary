namespace Reminders.CoreLibrary.Logic;
/// <summary>
/// this is for cases where something is reoccuring but only temporarily.
/// there can be several of them.
/// </summary>
public class TemporaryReoccuringReminderProcesses : BasicSubReminderProcesses
{
    private readonly ITemporaryReoccuringDataAccess _data;
    public TemporaryReoccuringReminderProcesses(ITemporaryReoccuringDataAccess data, ISnoozeDataAccess snoozeData) : base(data, snoozeData)
    {
        _data = data;
    }
    public override Task<ReminderModel?> GetNextReminderAsync()
    {
        return _data.GetNextTemporaryReoccuringReminderAsync();
    }
}