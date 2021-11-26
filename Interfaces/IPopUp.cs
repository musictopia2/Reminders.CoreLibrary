namespace Reminders.CoreLibrary.Interfaces;
public interface IPopUp
{
    Task LoadAsync(string title, string message);
    void PlaySound(int howOftenToRepeat);
    Func<Task>? ClosedAsync { get; set; }
    Func<TimeSpan, Task>? SnoozedAsync { get; set; }
    public bool SupportsSound { get; set; }
}