namespace Reminders.CoreLibrary.Models;
public class AppointmentModel : ISimpleDapperEntity
{
    public int ID { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; } = ""; //this is what would show up for the reminder
    public string Notes { get; set; } = ""; //this is the notes
    [Required] //had to keep datetime because of problems with databases and retrieving dateonly objects.
    public DateTime? AppointmentDate { get; set; } //this is the date of the appointment.
    public string ReminderTime { get; set; } = ""; //decided that time should be separate field.
    public override string ToString()
    {
        return $"{Title} on {AppointmentDate!.Value.ToShortDateString()}.  Reminder at {ReminderTime} ";
    }
}