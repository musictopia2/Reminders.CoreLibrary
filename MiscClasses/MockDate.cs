namespace Reminders.CoreLibrary.MiscClasses;
public class MockDate : IDatePicker
{
    private readonly Timer _timer;
    private static DateTime _currentTime; //since its mock, then can go ahead and make static.  if not using the class, then no problem.
    public static DateTime FutureDate { get; set; }
    DateTime IDatePicker.GetCurrentDate => _currentTime;
    public static Action<DateTime>? ShowNewTime { get; set; }
    public MockDate(DateTime startDate)
    {
        _currentTime = startDate;
        FutureDate = _currentTime;
        _timer = new Timer(1000);
        _timer.Elapsed += TimerElapsed;
        _timer.AutoReset = true;
        _timer.Start();
    }
    public static void ChangeDate(DateTime date)
    {
        _currentTime = date;
        FutureDate = _currentTime;
    }
    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _currentTime = _currentTime.AddSeconds(1);
        ShowNewTime?.Invoke(_currentTime);
    }
}