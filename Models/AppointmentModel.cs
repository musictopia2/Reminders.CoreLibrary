namespace Reminders.CoreLibrary.Models;
public partial class AppointmentModel : ISimpleDatabaseEntity
{
    public int ID { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = ""; //this is what would show up for the reminder
    public string Notes { get; set; } = ""; //this is the notes
    [Required] //trying for dateonly since there are ways to get dateonly from databases now.
    public DateOnly? AppointmentDate { get; set; } //this is the date of the appointment.
    public string ReminderTime { get; set; } = ""; //decided that time should be separate field.
    public override string ToString()
    {
        return $"{Title} on {AppointmentDate!.Value.ToShortDateString()}.  Reminder at {ReminderTime} ";
    }
}