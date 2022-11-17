namespace Reminders.CoreLibrary.Components;
public partial class ReminderLabelComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public string Section { get; set; } = "";
    [Parameter]
    public IReminderViewModel? DataContext { get; set; }
    private bool _didChange;
    protected override void OnInitialized()
    {
        DataContext!.CurrentDateText = "";
        MockDate.ShowNewTime = item =>
        {
            _didChange = true;
            DataContext!.CurrentDateText = item.ToString();
        };
        RunTask();
        base.OnInitialized();
    }
    private async void RunTask()
    {
        do
        {
            await Task.Delay(500);
            if (_didChange)
            {
                StateHasChanged();
                _didChange = false;
            }
        } while (true);
    }
}