namespace ANLG.Utilities.States.ModularStates;

public class ExitConditionModule<T> : IExitCondition
{
    public required Func<T, IState?> ExitCondition { get; init; }
    public required T Arg1 { get; init; }
    
    public IState? EvaluateExitConditions()
    {
        return ExitCondition(Arg1);
    }
}