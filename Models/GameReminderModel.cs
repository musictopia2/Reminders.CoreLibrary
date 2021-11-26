namespace Reminders.CoreLibrary.Models;
public class GameReminderModel : ISimpleDapperEntity
{
    public int ID { get; set; }
    public int HowMany { get; set; }
    public EnumTimeFormat Mode { get; set; }
}