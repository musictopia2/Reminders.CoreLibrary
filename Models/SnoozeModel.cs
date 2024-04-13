namespace Reminders.CoreLibrary.Models;
public partial class SnoozeModel : ISimpleDatabaseEntity
{
    public int ID { get; set; }
    public string Key { get; set; } = "";
    public string Reminder { get; set; } = "";
}