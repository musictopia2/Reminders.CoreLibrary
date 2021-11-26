namespace Reminders.CoreLibrary.DataAccess;
public interface IGameReminderDataAccess : IProcessedReminder
{
    Task<GameReminderModel?> GetReminderDataAsync();
    Task ToggleOnOffAsync();
    Task ToggleOnOffAsync(bool running);
    Task<bool> IsRunningAsync();
    Task AddGameReminderAsync(GameReminderModel reminder);
}