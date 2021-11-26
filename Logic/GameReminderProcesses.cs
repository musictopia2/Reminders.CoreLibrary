namespace Reminders.CoreLibrary.Logic;
public class GameReminderProcesses : BasicSubReminderProcesses
{
    private readonly IDatePicker _processedDate;
    private readonly IGameReminderDataAccess _reminderDataAccess;
    private readonly IGameFollowUp _gameFollow;
    private DateTime? _wakeDate;
    private DateTime? _nextDate;
    public GameReminderProcesses(ISnoozeDataAccess snoozeData,
        IDatePicker _processedDate,
        IGameReminderDataAccess reminderDataAccess,
        IGameFollowUp gameFollow) : base(snoozeData)
    {
        this._processedDate = _processedDate;
        _reminderDataAccess = reminderDataAccess;
        _gameFollow = gameFollow;
        ShowSounds = false;
    }
    public async Task StartReminderOverAsync()
    {
        _wakeDate = null;
        DateTime currentDate = _processedDate.GetCurrentDate;
        await ProcessNextDateAsync(currentDate);
    }
    public Task ResetDateAsync()
    {
        _nextDate = null;
        _wakeDate = null;
        return Task.CompletedTask;
    }
    public override async Task CloseReminderAsync(DateTime currentDate)
    {
        await DeleteSnoozeAsync();
        if (ShowSounds)
        {
            int seconds = await _gameFollow.SecondsToNextFollowUpAsync();
            _wakeDate = currentDate.AddSeconds(seconds);
        }
        else
        {
            await ResetDateAsync();
        }
    }
    private async Task ProcessNextDateAsync(DateTime currentDate)
    {
        GameReminderModel? data = await _reminderDataAccess.GetReminderDataAsync();
        if (data != null)
        {
            if (data.Mode == EnumTimeFormat.Days)
            {
                _nextDate = currentDate.AddDays(data.HowMany);
            }
            else if (data.Mode == EnumTimeFormat.Hours)
            {
                _nextDate = currentDate.AddHours(data.HowMany);
            }
            else if (data.Mode == EnumTimeFormat.Minutes)
            {
                _nextDate = currentDate.AddMinutes(data.HowMany);
            }
            else if (data.Mode == EnumTimeFormat.Seconds)
            {
                _nextDate = currentDate.AddSeconds(data.HowMany);
            }
        }
        else
        {
            _nextDate = null;
        }
    }
    public async Task FinishedGameAction()
    {
        if (_wakeDate == null)
        {
            return; 
        }
        await StartReminderOverAsync();
    }
    public override async Task<ReminderModel?> GetNextReminderAsync()
    {
        bool rets = await _reminderDataAccess.IsRunningAsync();
        if (rets == false)
        {
            return null;
        }
        ReminderModel output;
        if (_wakeDate != null)
        {
            output = new ReminderModel();
            output.Message = "Follow up reminder to finish game action and close out.";
            output.Title = "Game Reminder";
            output.NextDate = _wakeDate.Value;
            return output;
        }
        if (_nextDate == null)
        {
            DateTime currentDate = _processedDate.GetCurrentDate;
            await ProcessNextDateAsync(currentDate);
        }
        if (_nextDate == null)
        {
            return null;
        }
        output = new ReminderModel();
        output.Message = "Go into game to do some action";
        output.Title = "Game Reminder";
        output.NextDate = _nextDate.Value;
        return output;
    }
}