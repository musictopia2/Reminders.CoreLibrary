namespace Reminders.CoreLibrary.Logic;
public static class ReminderCreaters
{
    public static void AppendSameDayReminders(BasicList<ReminderModel> reminders, BasicList<SameDayModel> days)
    {
        days.ForEach(d => AppendSameDayReminder(reminders, d));
    }
    public static void AppendSameDayReminder(BasicList<ReminderModel> reminders, SameDayModel day)
    {
        ReminderModel reminder = new()
        {
            NextDate = day.ReminderDate,
            Message = day.Title
        };
        reminders.Add(reminder);
    }
    public static void AppendAppointments(BasicList<ReminderModel> reminders, BasicList<AppointmentModel> appointments, DateTime currentDate)
    {
        appointments.ForEach(a => AppendNewAppointment(reminders, a, currentDate));
    }
    public static void AppendNewAppointment(BasicList<ReminderModel> reminders, AppointmentModel appointment, DateTime currentDate)
    {
        DateOnly nextDate = appointment.AppointmentDate!.Value;
        DateTime time = DateTime.Parse(appointment.ReminderTime);
        DateTime remindDate = new(nextDate.Year, nextDate.Month, nextDate.Day, time.Hour, time.Minute, 0);
        if (remindDate > currentDate)
        {
            ReminderModel reminder = new()
            {
                Message = appointment.Title,
                NextDate = remindDate
            };
            reminders.Add(reminder);
        }
    }
    public static void AppendNewTemporaryReoccuringActivity(BasicList<ReminderModel> reminders, TemporaryReoccuringReminderModel activity, DateTime currentDate)
    {
        DateTime tryDate;
        tryDate = activity.StartDate;
        do
        {
            ReminderModel reminder = new()
            {
                Message = activity.Message,
                NextDate = tryDate
            };
            if (currentDate <= tryDate)
            {
                reminders.Add(reminder);
            }
            switch (activity.TimeMode)
            {
                case EnumTimeFormat.None:
                    break;
                case EnumTimeFormat.Minutes:
                    tryDate = tryDate.AddMinutes(activity.HowMany);
                    break;
                case EnumTimeFormat.Hours:
                    tryDate = tryDate.AddHours(activity.HowMany);
                    break;
                case EnumTimeFormat.Days:
                    tryDate = tryDate.AddDays(activity.HowMany);
                    break;
                case EnumTimeFormat.Seconds:
                    tryDate = tryDate.AddSeconds(activity.HowMany);
                    break;
                default:
                    throw new CustomBasicException("Does not support mode.  Rethink");
            }
            if (tryDate > activity.EndDate)
            {
                return;
            }
        } while (true);
    }
    public static void AppendReminders(BasicList<ReminderModel> reminders, BasicList<TemporaryReoccuringReminderModel> activities, DateTime currentDate)
    {
        foreach (var activity in activities)
        {
            AppendNewTemporaryReoccuringActivity(reminders, activity, currentDate);
        }
    }
}