namespace ANLG.Utilities.States;

public interface IStateMachineManager
{
    /// <summary>
    /// Adds a state machine to the manager.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws if state machine is not initialized</exception>
    void Add<TSearch>(IStateMachine stateMachine, bool isExact = false) where TSearch : IState;

    /// <summary>
    /// Performs <see cref="IStateMachine.DoCurrentStateActivity"/> on all contained state machines.
    /// </summary>
    void DoAllStateMachineActivity();
    
    /// <summary>
    /// Uninitializes all contained state machines.
    /// </summary>
    void ShutDown();
}