namespace Reminders.CoreLibrary.Interfaces;
public interface IGameFollowUp
{
    Task<int> SecondsToNextFollowUpAsync();
}