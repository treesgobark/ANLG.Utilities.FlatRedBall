namespace ANLG.Utilities.States;

/// <summary>
/// </summary>
public class StateMachine : IStateMachine
{
    protected IState? ExitOverride { get; set; }

    /// <inheritdoc/>
    public bool IsRunning => CurrentState != EmptyState.Instance;

    /// <summary>
    /// All the states that belong to this collection (state machine)
    /// </summary>
    protected List<IState> States { get; } = new();

    /// <summary>
    /// The currently active state
    /// </summary>
    protected IState CurrentState { get; set; } = EmptyState.Instance;
    
    /// <inheritdoc/>
    public IStateMachine Add(IState state)
    {
        if (States.Any(existing => existing.GetType() == state.GetType()))
        {
            throw new ArgumentException($"Collection already has a state of type {state.GetType().Name}");
        }
        States.Add(state);

        return this;
    }

    /// <inheritdoc/>
    public IState Get<TSearch>(bool isExact = false) where TSearch : IState
    {
        foreach (var state in States)
        {
            if (Type.GetTypeHandle(EmptyState.Instance).Value == typeof(TSearch).TypeHandle.Value)
            {
                return EmptyState.Instance;
            }
            
            if (isExact && Type.GetTypeHandle(state).Value == typeof(TSearch).TypeHandle.Value)
            {
                return state;
            }
            
            if (!isExact && state is TSearch)
            {
                return state;
            }
        }
        
        throw new ArgumentException($"State machine does not contain any states of type {typeof(TSearch).Name}");
    }

    /// <inheritdoc/>
    public void SetStartingState<TSearch>(bool isExact = false) where TSearch : IState
    {
        if (IsRunning)
        {
            throw new InvalidOperationException("Cannot set starting state when state machine is running.");
        }
        
        ExitOverride = Get<TSearch>(isExact);
    }

    /// <inheritdoc/>
    public void SetStartingState(IState state)
    {
        ExitOverride = state;
    }

    /// <inheritdoc/>
    public void DoCurrentStateActivity()
    {
        AdvanceCurrentState();
        CurrentState.CustomActivity();
    }

    /// <inheritdoc/>
    public void AdvanceCurrentState()
    {
        var newState = ExitOverride ?? CurrentState.EvaluateExitConditions();
        ExitOverride = default;

        int count = 0;
        while (newState is not null)
        {
            if (count++ > 100)
            {
                throw new StackOverflowException($"The state collection {GetType().Name} has reached the exit condition limit. " +
                                                 $"The current state, {CurrentState.GetType().Name}, is trying to go to " +
                                                 $"{newState.GetType().Name}. For more information, consult the innerException.",
                                                 new Exception("Your states have likely encountered an infinite loop of exit conditions. State collections" +
                                                               " will try to cycle to the next state via their exit conditions continuously until it reaches " +
                                                               "a state that returns null from EvaluateExitConditions."));
            }
            
            CurrentState.BeforeDeactivate();
            newState.OnActivate();
            CurrentState = newState;

            newState = CurrentState.EvaluateExitConditions();
        }
    }

    /// <inheritdoc/>
    public void ShutDown()
    {
        ExitOverride = EmptyState.Instance;
        AdvanceCurrentState();
    }

    // write log for all previous states entered
    // and you know what fuck it ill also write down the thing about entering state machines within state machines like with a stack like magic lol
}
