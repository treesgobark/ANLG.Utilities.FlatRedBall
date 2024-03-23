namespace ANLG.Utilities.FlatRedBall.States;

public interface IStateMachine
{
    /// <summary>
    /// The currently active state
    /// </summary>
    IState CurrentState { get; }
    
    /// <summary>
    /// Adds a state to the collection.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException for duplicate types.</exception>
    void Add(IState state);

    /// <summary>
    /// Returns the state in this collection with the exact type <typeparamref name="TSearch"/>.
    /// Returns the first state in this collection whose type is assignable to <typeparamref name="TSearch"/>.
    /// </summary>
    /// <exception cref="ArgumentException">Throws ArgumentException if the collection doesn't have a state of the given type.</exception>
    IState Get<TSearch>(bool isExact = false) where TSearch : IState;

    /// <summary>
    /// Sets the current state to the one of type <typeparamref name="TSearch"/> in this collection,
    ///   then calls <see cref="EntityState{T,TSelf}.OnActivate"/> on it.
    ///   Must be called before any state activity can happen.
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
    /// Forces the state machine to move to the given state by replacing the next exit condition check.
    /// </summary>
    void OverrideState<TState>(bool isExact = false) where TState : IState;
}
