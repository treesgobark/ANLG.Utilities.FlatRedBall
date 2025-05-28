namespace ANLG.Utilities.Core.States;

/// <summary>
/// </summary>
public class StateMachine : IStateMachine
{
    protected IState? ExitOverride { get; set; }

    /// <inheritdoc/>
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// All the states that belong to this collection (state machine)
    /// </summary>
    protected List<IState> States { get; } = new();

    /// <summary>
    /// The currently active state
    /// </summary>
    protected IState CurrentState { get; set; } = EmptyState.Instance;
    
    /// <summary>
    /// Adds a state to the collection.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException for duplicate types.</exception>
    public void Add(IState state)
    {
        if (States.Any(existing => existing.GetType() == state.GetType()))
        {
            throw new ArgumentException($"Collection already has a state of type {state.GetType().Name}");
        }
        States.Add(state);
    }

    public IState Get<TSearch>(bool isExact = false) where TSearch : IState
    {
        foreach (var state in States)
        {
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

    /// <summary>
    /// Sets the current state to the one of type <typeparamref name="TSearch"/> in this collection,
    ///   then calls <see cref="EntityState{T,TSelf}.OnActivate"/> on it.
    ///   Must be called before any state activity can happen.
    /// </summary>
    public void InitializeStartingState<TSearch>(bool isExact = false) where TSearch : IState
    {
        if (IsInitialized)
        {
            throw new InvalidOperationException($"State machine already initialized.");
        }
        
        States.ForEach(c => c.Initialize());
        
        ExitOverride   = Get<TSearch>(isExact);
        IsInitialized = true;
    }

    /// <inheritdoc/>
    public void DoCurrentStateActivity()
    {
        EvaluateExitConditions();
        CurrentState.CustomActivity();
    }

    /// <inheritdoc/>
    public void EvaluateExitConditions()
    {
        if (!IsInitialized)
        {
            throw new InvalidOperationException($"You must initialize collection with "
                                                + $"{nameof(InitializeStartingState)} before performing activity.");
        }

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
            
            CurrentState.BeforeDeactivate(newState);
            CurrentState = newState;
            CurrentState.OnActivate();

            newState = CurrentState.EvaluateExitConditions();
        }
    }

    /// <summary>
    /// Forces the state machine to move to the given state by replacing the next exit condition check.
    /// </summary>
    public void OverrideState<TState>(bool isExact = false) where TState : IState
    {
        ExitOverride = Get<TState>(isExact);
    }

    public void Uninitialize()
    {
        if (!IsInitialized)
        {
            throw new InvalidOperationException($"You must initialize collection with "
                                                + $"{nameof(InitializeStartingState)} before calling {nameof(Uninitialize)}.");
        }

        IsInitialized = false;
        
        CurrentState.BeforeDeactivate(null);
        CurrentState = EmptyState.Instance;

        foreach (var state in States)
        {
            state.Uninitialize();
        }
    }

    // write log for all previous states entered
    // and you know what fuck it ill also write down the thing about entering state machines within state machines like with a stack like magic lol
}
