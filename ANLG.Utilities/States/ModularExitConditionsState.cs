using ANLG.Utilities.Core;

namespace ANLG.Utilities.States;

public abstract class ModularExitConditionsState : IState
{
    private SortedList<Priority, List<ExitCondition>> _exitConditions = new();

    public void AddExitCondition(ExitCondition exitCondition)
    {
        if (_exitConditions.TryGetValue(exitCondition.Priority, out var exitConditions))
        {
            exitConditions.Add(exitCondition);
        }
        else
        {
            _exitConditions[exitCondition.Priority] = [exitCondition];
        }
    }
    
    public abstract void Initialize();
    public abstract void OnActivate(IState? previousState);
    public abstract void CustomActivity();

    public IState? EvaluateExitConditions()
    {
        foreach (var (_, exitConditions) in _exitConditions)
        {
            foreach (ExitCondition exitCondition in exitConditions)
            {
                if (exitCondition.Condition())
                {
                    return exitCondition.NextState;
                }
            }
        }

        return null;
    }
    
    public abstract void BeforeDeactivate(IState? nextState);
    public abstract void Uninitialize();
}

public class ExitCondition : IComparable<ExitCondition>
{
    public Priority Priority { get; set; }
    public Func<bool> Condition { get; set; }
    public IState? NextState { get; set; }

    public int CompareTo(ExitCondition? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        return Priority.CompareTo(other.Priority);
    }
}