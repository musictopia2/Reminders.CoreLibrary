namespace Reminders.CoreLibrary.DataAccess;
public interface ISimpleWeeklyDataAccess : IProcessedReminder
{
    Task<ReminderModel?> GetNextWeeklyReminderAsync();
    Task AddNewWeeklyReminderAsync(WeeklyReminderModel model);
    Task<BasicList<WeeklyReminderModel>> GetWeeklyReminderListAsync();
    Task DeleteWeeklyReminderAsync(WeeklyReminderModel model);
    Task UpdateWeeklyReminderAsync(WeeklyReminderModel model);
}