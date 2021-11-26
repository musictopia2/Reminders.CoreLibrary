namespace Reminders.CoreLibrary.DataAccess;
/// <summary>
/// this is a case where you just want a one time reminder but not an appointment.
/// </summary>
public interface ISameDayReminderDataAccess : IProcessedReminder
{
    Task<ReminderModel?> GetNextReminderAsync();
    Task AddNewSameDayReminderAsync(SameDayModel model);
    Task<BasicList<SameDayModel>> GetSameDayReminderListAsync();
    Task DeleteSameDayReminderAsync(SameDayModel model);
    Task UpdateSameDayReminderAsync(SameDayModel model);
}