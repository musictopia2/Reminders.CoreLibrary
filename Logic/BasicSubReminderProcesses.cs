namespace Reminders.CoreLibrary.Logic;
public abstract class BasicSubReminderProcesses : ISubReminder, IAdjustNextDate
{
    private bool _started = false;
    public BasicSubReminderProcesses(IProcessedReminder processed, ISnoozeDataAccess snoozeData)
    {
        _processed = processed;
        _snoozeData = snoozeData;
        InitAsync();
    }
    private async void InitAsync()
    {

        MainReminderProcesses.AddReminder(this);
        _nextReminder = await _snoozeData.GetSnoozedReminderAsync(ToString()!);
        if (_nextReminder != null)
        {
            Snoozing = true;
            NextDate = _nextReminder.NextDate;
        }
        _started = true;
    }
    public BasicSubReminderProcesses(ISnoozeDataAccess snoozeData)
    {
        _snoozeData = snoozeData;
        InitAsync();
    }
    public bool ShowSounds { get; set; } = true;
    public int HowOftenToRepeat { get; set; } = 10;
    public DateTime? NextDate { get; private set; }
    protected bool Snoozing;
    private ReminderModel? _nextReminder;
    private readonly IProcessedReminder? _processed;
    private readonly ISnoozeDataAccess _snoozeData;
    public Action<ReminderModel?>? UpdateNextReminder { get; set; }
    protected async Task DeleteSnoozeAsync()
    {
        if (Snoozing)
        {
            await _snoozeData.DeleteSnoozeAsync(ToString()!);
        }
    }
    public virtual async Task CloseReminderAsync(DateTime currentDate)
    {
        await DeleteSnoozeAsync();
        var model = await GetNextReminderAsync();
        UpdateNextReminder?.Invoke(model);
        Snoozing = false;
    }
    public Task SnoozeAsync(TimeSpan time, DateTime currentDate)
    {

        NextDate = currentDate.Add(time);
        if (_nextReminder == null)
        {
            throw new CustomBasicException("There was no next reminder to even save.  Rethink");
        }
        if (Snoozing)
        {
            return _snoozeData.UpdateSnooozeAsync(ToString()!, NextDate.Value);
        }
        else
        {
            Snoozing = true;
            return _snoozeData.SaveSnoozeAsync(ToString()!, _nextReminder, NextDate.Value);
        }
    }
    public abstract Task<ReminderModel?> GetNextReminderAsync();
    public virtual async Task<(bool needsReminder, string title, string message)> GetReminderInfoAsync(DateTime currentDate)
    {
        if (_started == false)
        {
            do
            {
                if (_started)
                {
                    break;
                }
                await Task.Delay(10);
            } while (true);
        }
        if (Snoozing == false)
        {
            _nextReminder = await GetNextReminderAsync();
            if (_nextReminder == null)
            {
                NextDate = null;
            }
            else
            {
                NextDate = _nextReminder.NextDate;
            }
            if (NextDate == null)
            {
                return (false, "", "");
            }
        }
        else
        {
            if (NextDate == null)
            {
                throw new CustomBasicException("Next date cannot be null when snoozing.  Rethink");
            }
        }
        if (_nextReminder != null && currentDate >= NextDate)
        {
            return (true, _nextReminder.Title, _nextReminder.Message);
        }
        return (false, "", "");

    }
    public virtual Task ProcessedReminderAsync()
    {
        if (_processed == null)
        {
            return Task.CompletedTask;
        }
        return _processed.ProcessedReminderAsync();
    }
    protected async Task AdjustMinutesAsync(int minutes)
    {
        if (Snoozing)
        {
            if (NextDate == null)
            {
                throw new CustomBasicException("Can't adjust minutes for snooze because next date is null.  Rethink");
            }
            NextDate = NextDate.Value.AddMinutes(minutes);
            await _snoozeData.UpdateSnooozeAsync(ToString()!, NextDate.Value);
            MainReminderProcesses.Refresh();
            return;
        }
        _nextReminder = await GetNextReminderAsync();
        if (_nextReminder == null)
        {
            throw new CustomBasicException("Can't snooze minutes because no reminder.  Rethink");
        }
        Snoozing = true;
        NextDate = _nextReminder.NextDate.AddMinutes(minutes);
        await _snoozeData.SaveSnoozeAsync(ToString()!, _nextReminder, NextDate.Value);
        MainReminderProcesses.Refresh();
    }
    Task IAdjustNextDate.AdjustMinutesAsync(int minutes)
    {
        return AdjustMinutesAsync(minutes);
    }
    protected async Task AdjustTimeAsync(DateTime time)
    {
        DateTime tempDate;
        if (Snoozing)
        {
            if (NextDate == null)
            {
                throw new CustomBasicException("Can't adjust minutes for snooze because next date is null.  Rethink");
            }
            tempDate = NextDate.Value;
            NextDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, time.Hour, time.Minute, 0);
            await _snoozeData.UpdateSnooozeAsync(ToString()!, NextDate.Value);
            MainReminderProcesses.Refresh();
            return;
        }
        _nextReminder = await GetNextReminderAsync();
        if (_nextReminder == null)
        {
            throw new CustomBasicException("Can't snooze minutes because no reminder.  Rethink");
        }
        Snoozing = true;
        NextDate = _nextReminder.NextDate;
        tempDate = NextDate.Value;
        NextDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, time.Hour, time.Minute, 0);
        await _snoozeData.SaveSnoozeAsync(ToString()!, _nextReminder, NextDate.Value);
        MainReminderProcesses.Refresh();
    }
    Task IAdjustNextDate.AdjustTimeAsync(DateTime time)
    {
        return AdjustTimeAsync(time);
    }
}