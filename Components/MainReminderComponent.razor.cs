namespace Reminders.CoreLibrary.Components;
public partial class MainReminderComponent
{
    [Inject]
    private MainViewModel? DataContext { get; set; }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    protected override void OnInitialized()
    {
        DataContext!.StateChanged = () => InvokeAsync(StateHasChanged);
    }
}