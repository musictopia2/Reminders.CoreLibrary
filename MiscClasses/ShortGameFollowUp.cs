namespace Reminders.CoreLibrary.MiscClasses;
public class ShortGameFollowUp : IGameFollowUp
{
    Task<int> IGameFollowUp.SecondsToNextFollowUpAsync()
    {
        return Task.FromResult(5); //short is 5 seconds for testing.
    }
}