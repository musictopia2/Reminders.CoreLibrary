namespace Reminders.CoreLibrary.Models;
public partial class TemporaryReoccuringReminderModel : ISimpleDatabaseEntity
{
    public int ID { get; set; }
    public EnumTimeFormat TimeMode { get; set; } = EnumTimeFormat.Hours;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Range(1, 100, ErrorMessage = "Must enter a number between 1 and 100")]
    public int HowMany { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Message { get; set; } = "";
    [NotMapped]
    public string StartTime { get; set; } = "";
    [NotMapped]
    public string EndTime { get; set; } = "";
    public override string ToString()
    {
        if (TimeMode == EnumTimeFormat.Days)
        {
            return $"{Message} starting {StartDate} ending {EndDate} every {HowMany} days";
        }
        if (TimeMode == EnumTimeFormat.Hours)
        {
            return $"{Message} starting {StartDate} ending {EndDate} every {HowMany} hours";
        }
        return base.ToString()!;
    }
}