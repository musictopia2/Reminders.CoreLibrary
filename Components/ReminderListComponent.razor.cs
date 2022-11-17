namespace Reminders.CoreLibrary.Components;
public partial class ReminderListComponent<T>
{
    [Parameter]
    public BasicList<T> ItemList { get; set; } = new();
}