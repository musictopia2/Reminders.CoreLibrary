namespace Reminders.CoreLibrary.Models;
public class VariableCycleModel
{
    public EnumTimeFormat TimeFormat { get; set; }
    public int HowLong { get; set; } = 0; //try to keep as integer (?)
    public string Title { get; set; } = "";
    public string Message { get; set; } = "";
}