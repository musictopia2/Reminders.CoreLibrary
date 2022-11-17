namespace Reminders.CoreLibrary.Helpers;
public class ReminderContainer
{
    public string Message { get; set; } = "";
    public bool SupportsSnooze { get; set; } //android will not support snoozing.
    public Action? ClosePopup { get; set; }
}