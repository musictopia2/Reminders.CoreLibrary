namespace Reminders.CoreLibrary.DataAccess;
public interface ITemporaryReoccuringDataAccess : IProcessedReminder
{
    Task<ReminderModel?> GetNextTemporaryReoccuringReminderAsync();
    //attempt to have the extra stuff done here.
    /// <summary>
    /// this is a method to add to the temporary reoccuring activity list
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task AddNewTemporaryReoccuringActivityAsync(TemporaryReoccuringReminderModel model);
    Task<BasicList<TemporaryReoccuringReminderModel>> GetReoccuringActivityListAsync();
    Task DeleteTemporaryReoccuringActivityAsync(TemporaryReoccuringReminderModel model);
    Task UpdateTemporaryReoccuringActityAsync(TemporaryReoccuringReminderModel model);
}