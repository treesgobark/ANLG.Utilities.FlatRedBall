namespace ANLG.Utilities.States.ModularStates;

public interface IDurationModule : ITimerModule
{
    TimeSpan Duration { get; }
    float NormalizedProgress { get; }
    bool HasDurationCompleted { get; }
}