namespace Reminders.CoreLibrary.Models;
public class BaseReminderModel
{
    public EnumReminderStatus ReminderStatus { get; set; } = EnumReminderStatus.None;
    public int UpTo { get; set; }
    public DateTime? TimeStarted { get; set; } //for now, can't risk doing timeonly until database support is completed.
    public DateTime? TimeEnded { get; set; }
    public VariableCycleModel Currentreminder { get; set; } = new VariableCycleModel();
}