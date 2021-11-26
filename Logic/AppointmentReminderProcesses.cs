namespace Reminders.CoreLibrary.Logic;
public class AppointmentReminderProcesses : BasicSubReminderProcesses
{
    private readonly IAppointmentDataAccess _data;
    public AppointmentReminderProcesses(IAppointmentDataAccess data, ISnoozeDataAccess snoozeData) : base(data, snoozeData)
    {
        _data = data;
    }
    public override Task<ReminderModel?> GetNextReminderAsync()
    {
        return _data.GetNextAppointmentReminderAsync();
    }
}