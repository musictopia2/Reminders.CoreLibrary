namespace Reminders.CoreLibrary.Interfaces;
public interface IReminderBasicData
{
    string StartText { get; }
    string ContinueText { get; }
    string Title { get; }
    int HowManyCycles { get; }
}