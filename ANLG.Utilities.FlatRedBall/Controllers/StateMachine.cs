namespace ANLG.Utilities.FlatRedBall.States;

/// <summary>
/// </summary>
public class StateMachine : IStateMachine
{
    private bool _isInitialized = false;

    protected IState? ExitOverride { get; set; }

    /// <summary>
    /// All the states that belong to this collection (state machine)
    /// </summary>
    protected List<IState> States { get; } = new();
    
    /// <summary>
    /// The currently active state
    /// </summary>
    public IState CurrentState { get; protected set; }
    
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
        States.ForEach(c => c.Initialize());
        
        CurrentState   = Get<TSearch>(isExact);
        ExitOverride   = CurrentState;
        _isInitialized = true;
    }

    /// <summary>
    /// Evaluates the exit conditions of the current state, then if a state switch happens,
    ///   <see cref="EntityState{T,TSelf}.BeforeDeactivate"/> is called on the old state,
    ///   then <see cref="EntityState{T,TSelf}.OnActivate"/> and <see cref="EntityState{T,TSelf}.CustomActivity"/>
    ///   are called on the new state, in that order.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws InvalidOperationException if collection is uninitialized.</exception>
    public void DoCurrentStateActivity()
    {
        if (!_isInitialized)
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
            
            CurrentState.BeforeDeactivate();
            CurrentState = newState;
            CurrentState.OnActivate();

            newState = CurrentState.EvaluateExitConditions();
        }
        
        CurrentState.CustomActivity();
    }

    /// <summary>
    /// Forces the state machine to move to the given state by replacing the next exit condition check.
    /// </summary>
    public void OverrideState<TState>(bool isExact = false) where TState : IState
    {
        ExitOverride = Get<TState>(isExact);
    }
    
    // write log for all previous states entered
    // and you know what fuck it ill also write down the thing about entering state machines within state machines like with a stack like magic lol
}
