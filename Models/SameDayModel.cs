namespace Reminders.CoreLibrary.Models;
public class SameDayModel : ISimpleDapperEntity
{
    public int ID { get; set; }
    public string Title { get; set; } = "";
    public DateTime ReminderDate { get; set; }
    [NotMapped]
    public string TimeText { get; set; } = "";
    [NotMapped]
    [Range(0, 100, ErrorMessage = "Must enter from 0 to 100 for units")]
    public int HowMany { get; set; }
    public override string ToString()
    {
        return $"{Title} on {ReminderDate}";
    }
}