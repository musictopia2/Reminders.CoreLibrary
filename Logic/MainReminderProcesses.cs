namespace Reminders.CoreLibrary.Logic;
public static class MainReminderProcesses
{
    static Timer? _timer;
    private static IPopUp? _currentPopUp;
    private static readonly BasicList<ISubReminder> _reminderList = new();
    private static ISubReminder? _currentReminder;
    public static Action<string>? ShowNextDate { get; set; }
    public static Func<Task>? UserCompletedAction { get; set; }
    private static IDatePicker? _dateUsed;
    private static bool _waitingForUser;
    /// <summary>
    /// in the case of the bible program, when clicking, then would be set to false.
    /// no reminders can be processed while waiting for user.
    /// </summary>
    public static bool WaitingForUser
    {
        get
        {
            return _waitingForUser;
        }
        set
        {
            if (_waitingForUser == false && value == false)
            {
                return;
            }
            if (value == true)
            {
                _waitingForUser = true;
                return;
            }
            _waitingForUser = false;
            if (UserCompletedAction == null)
            {
                throw new CustomBasicException("No action can be invoked for waiting for user because nothing registered.  Rethink");
            }
            UserCompletedAction.Invoke();
            _timer!.Start();
        }
    }
    public static void AddReminder(ISubReminder reminder)
    {
        _reminderList.Add(reminder);
    }
    public static void Refresh()
    {
        var reminder = _reminderList.Where(x => x.NextDate.HasValue == true).OrderBy(x => x.NextDate!.Value).FirstOrDefault();
        if (reminder == null)
        {
            ShowNextDate?.Invoke("No Reminders Set");
            return;
        }
        ShowNextDate?.Invoke(reminder.NextDate!.Value.ToString());
    }
    public async static Task RecalculateRemindersAsync()
    {
        foreach (var rr in _reminderList)
        {
            await rr.GetReminderInfoAsync(_dateUsed!.GetCurrentDate);
        }
        Refresh();
    }
    public static async Task InitAsync(IDatePicker picker, IPopUp pop)
    {
        _dateUsed = picker;
        _currentPopUp = pop;
        _timer = new Timer(1000)
        {
            AutoReset = false
        };
        _timer.Elapsed += OnTimerElapsed;
        await RecalculateRemindersAsync();
        _timer.Start();
    }

    private async static void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        if (WaitingForUser)
        {
            return;
        }
        try
        {
            await Execute.OnUIThreadAsync(RunProcessAsync);
        }
        catch (TaskCanceledException)
        {
            return;
        }
    }
    private static async Task RunProcessAsync()
    {
        foreach (var rr in _reminderList)
        {
            var (needsReminder, title, message) = await rr.GetReminderInfoAsync(_dateUsed!.GetCurrentDate);
            if (needsReminder)
            {
                _currentReminder = rr;
                await rr.ProcessedReminderAsync();
                _currentPopUp!.SupportsSound = _currentReminder.ShowSounds;
                _currentPopUp.ClosedAsync = CurrentPopupClosed;
                _currentPopUp.SnoozedAsync = CurrentPopUpSnoozed;
                await _currentPopUp!.LoadAsync(title, message);
                if (_currentReminder.ShowSounds)
                {
                    _currentPopUp.PlaySound(rr.HowOftenToRepeat);
                }
                return;
            }
        }
        _timer!.Start();
    }
    private static async Task CurrentPopUpSnoozed(TimeSpan arg)
    {
        ClosePopups();
        await _currentReminder!.SnoozeAsync(arg, _dateUsed!.GetCurrentDate);
        ContinueChecking();
        Refresh();
    }
    private static async Task CurrentPopupClosed()
    {
        ClosePopups();
        await _currentReminder!.CloseReminderAsync(_dateUsed!.GetCurrentDate);
        await RecalculateRemindersAsync();
        ContinueChecking();
    }
    private static void ContinueChecking()
    {
        _currentReminder = null;
        _timer!.Start();
    }
    private static void ClosePopups()
    {
        if (_currentPopUp == null)
        {
            throw new CustomBasicException("There was no popup to even close.  Rethink");
        }
        _currentPopUp.ClosedAsync = null;
        _currentPopUp.SnoozedAsync = null;
    }
}