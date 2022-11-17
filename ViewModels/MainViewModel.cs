namespace Reminders.CoreLibrary.ViewModels;
public class MainViewModel : IReminderViewModel
{
    public string NextDayText { get; set; } = "";
    public string CurrentDateText { get; set; } = "";
    public Action? StateChanged { get; set; }
    public MainViewModel()
    {
        MainReminderProcesses.ShowNextDate = x =>
        {
            NextDayText = x;
            StateChanged?.Invoke();
        };
        MainReminderProcesses.Refresh();
    }
}