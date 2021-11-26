namespace Reminders.CoreLibrary.Interfaces;
public interface IReminderVariableData : IReminderBasicData
{
    BasicList<VariableCycleModel> GetVariableList();
}