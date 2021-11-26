namespace Reminders.CoreLibrary.Interfaces;

/// <summary>
/// the purpose of this one is having a native process that runs which external systems can tell it when to open/close.
/// </summary>
public interface INativeProcess
{
    void Open();
    void Close();
    void ProcessAction(EnumKey key);
}