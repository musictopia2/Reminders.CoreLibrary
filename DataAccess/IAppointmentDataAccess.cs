namespace Reminders.CoreLibrary.DataAccess;
public interface IAppointmentDataAccess : IProcessedReminder
{
    Task<ReminderModel?> GetNextAppointmentReminderAsync();
    Task AddNewAppointmentAsync(AppointmentModel model);
    Task<BasicList<AppointmentModel>> GetAppointmentListAsync();
    Task DeleteAppointmentAsync(AppointmentModel model);
    Task UpdateWeeklyReminderAsync(AppointmentModel model);
}