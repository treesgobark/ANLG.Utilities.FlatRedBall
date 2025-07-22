using ANLG.Utilities.Core;

namespace ANLG.Utilities.States;

/// <inheritdoc cref="TimedState"/>
public abstract class DurationState : TimedState
{
    protected virtual TimeSpan Duration { get; }

    protected float NormalizedProgress => (float)(TimeInState / Duration).Saturate();
    
    protected bool IsComplete => NormalizedProgress >= 1;
    
    protected DurationState(IReadonlyStateMachine states, ITimeManager timeManager) : base(states, timeManager)
    {
    }
}
