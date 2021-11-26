namespace Reminders.CoreLibrary.MiscClasses;
public class StandardGameFollowUp : IGameFollowUp
{
    Task<int> IGameFollowUp.SecondsToNextFollowUpAsync()
    {
        return Task.FromResult(90); //standard should be 90 seconds.  anybody can create other processes to figure out how long to wait for followup.
    }
}