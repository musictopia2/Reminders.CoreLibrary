namespace Reminders.CoreLibrary.Components;
public partial class ReminderPopupComponent
{
    [Inject]
    [AllowNull]
    private IPopUp Pop { get; set; }
    [CascadingParameter]
    [AllowNull]
    private MediaQueryListComponent Media { get; set; }
    [Inject]
    [AllowNull]
    private ReminderContainer Container { get; set; }
    private int _snoozeTime;
    private static string GetSnoozeText => GlobalHelpers.PopUpMode switch
    {
        EnumPopupMode.Minutes => "Snooze (In Minutes)",
        EnumPopupMode.Seconds => "Snooze (In Seconds)",
        EnumPopupMode.Hours => "Snooze (In Hours)",
        _ => throw new CustomBasicException("No mode set for snoozing")
    };
    private async Task CloseAsync()
    {
        PrivateClose();
        await Pop!.ClosedAsync?.Invoke()!;
    }
    private async Task SnoozeAsync()
    {
        if (_snoozeTime <= 0)
        {
            _snoozeTime = 0;
            return;
        }
        TimeSpan time;
        if (GlobalHelpers.PopUpMode == EnumPopupMode.Seconds)
        {
            time = new TimeSpan(0, 0, _snoozeTime);
        }
        else if (GlobalHelpers.PopUpMode == EnumPopupMode.Minutes)
        {
            time = new TimeSpan(0, _snoozeTime, 0);
        }
        else if (GlobalHelpers.PopUpMode == EnumPopupMode.Hours)
        {
            time = new TimeSpan(_snoozeTime, 0, 0);
        }
        else
        {
            throw new CustomBasicException("Unable to snooze because no mode set");
        }
        PrivateClose();
        try
        {
            await Pop!.SnoozedAsync?.Invoke(time)!;
        }
        catch (Exception ex)
        {
            throw new CustomBasicException(ex.Message);
        }
    }
    private void PrivateClose()
    {
        Container!.ClosePopup?.Invoke();
    }
}