namespace Reminders.CoreLibrary.ViewModels;
public abstract class BaseSubReminderViewModel : IReminderViewModel
{
    protected BasicSubReminderProcesses? BaseReminder;
    public string NextDayText { get; set; } = "No Reminders Set";
    public string CurrentDateText { get; set; } = "";
    public Action? StateChanged { get; set; }
    public virtual async Task InitAsync()
    {
        if (BaseReminder == null)
        {
            throw new CustomBasicException(
                "You never sent in a base reminder. That is required so it can get the message from one particular reminder process");
        }

        BaseReminder.UpdateNextReminder = PrivateUpdate;
        await RecalculateReminderAsync();
    }

    protected async Task RecalculateReminderAsync()
    {
        var reminder = await BaseReminder!.GetNextReminderAsync();
        PopulateItem(reminder);
    }
    private void PrivateUpdate(ReminderModel? reminder)
    {
        PopulateItem(reminder);
        StateChanged?.Invoke();
    }
    private void PopulateItem(ReminderModel? reminder)
    {
        if (reminder == null)
        {
            NextDayText = "No Reminders Set";
        }
        else
        {
            NextDayText = reminder.NextDate.ToString();
        }
    }
}