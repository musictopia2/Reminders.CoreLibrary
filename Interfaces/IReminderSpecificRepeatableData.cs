namespace Reminders.CoreLibrary.Interfaces;
public interface IReminderSpecificRepeatableData : IReminderBasicData
{
    int HowLongBetweenCycles { get; }
    string ReminderTitle { get; }
    string ReminderMessage { get; }
    EnumTimeFormat TimeFormat { get; }
}