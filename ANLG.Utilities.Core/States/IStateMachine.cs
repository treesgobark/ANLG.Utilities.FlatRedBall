namespace ANLG.Utilities.Core.States;

public interface IStateMachine : IReadonlyStateMachine
{
    /// <summary>
    /// Adds a state to the collection.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException for duplicate types.</exception>
    void Add(IState state);

    /// <summary>
    /// Initializes all states in the machine, then overrides the machine to move to the given state,
    /// <typeparamref name="TSearch"/>, on the first <see cref="DoCurrentStateActivity"/> call.
    /// </summary>
    void InitializeStartingState<TSearch>(bool isExact = false) where TSearch : IState;

    /// <summary>
    /// Evaluates the exit conditions of the current state, then if a state switch happens,
    ///   <see cref="EntityState{T,TSelf}.BeforeDeactivate"/> is called on the old state,
    ///   then <see cref="EntityState{T,TSelf}.OnActivate"/> and <see cref="EntityState{T,TSelf}.CustomActivity"/>
    ///   are called on the new state, in that order.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws InvalidOperationException if collection is uninitialized.</exception>
    void DoCurrentStateActivity();

    /// <summary>
    /// Evaluates the exit conditions of the current state, then if a state switch happens,
    ///   <see cref="EntityState{T,TSelf}.BeforeDeactivate"/> is called on the old state,
    ///   then <see cref="EntityState{T,TSelf}.OnActivate"/> is called on the new state. This process repeats until
    ///   a stable state has been reached.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws InvalidOperationException if collection is uninitialized.</exception>
    void AdvanceCurrentState();

    /// <summary>
    /// Forces the state machine to move to the given state by replacing the next exit condition check.
    /// </summary>
    void OverrideState<TState>(bool isExact = false) where TState : IState;
    
    /// <summary>
    /// Uninitializes all the states in the collection so this state machine can be safely destroyed
    /// </summary>
    void Uninitialize();
}

public interface IReadonlyStateMachine
{
    /// <summary>
    /// Indicates whether the state machine is ready to perform activity
    /// </summary>
    bool IsInitialized { get; }
    
    /// <summary>
    /// Indicates whether the state machine is currently traversing a path. Typically, this is equivalent to the
    /// current state being something other than <see cref="EmptyState"/>.
    /// </summary>
    bool IsRunning { get; }
    
    /// <summary>
    /// Returns the state in this collection with the exact type <typeparamref name="TSearch"/>.
    /// Returns the first state in this collection whose type is assignable to <typeparamref name="TSearch"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException if the collection doesn't have a state of the given type.</exception>
    IState Get<TSearch>(bool isExact = false) where TSearch : IState;
}