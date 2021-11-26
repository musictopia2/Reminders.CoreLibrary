namespace Reminders.CoreLibrary.Models;
public class ReminderModel
{
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
    public DateTime NextDate { get; set; }
}