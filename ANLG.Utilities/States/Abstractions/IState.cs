using System.Diagnostics.Contracts;

namespace ANLG.Utilities.States;

/// <summary>
/// A self-contained unit of logic that is relevant only while this state is active. Designed to be used by <see cref="IStateMachine"/>
///   Based on the object-oriented state pattern: <a href="https://refactoring.guru/design-patterns/state">here</a>.
/// </summary>
public interface IState : IActivate, IUpdate, IExitCondition, IDeactivate
{
}
