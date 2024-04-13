namespace Reminders.CoreLibrary.Models;
public partial class GameReminderModel : ISimpleDatabaseEntity
{
    public int ID { get; set; }
    public int HowMany { get; set; }
    public EnumTimeFormat Mode { get; set; }
}