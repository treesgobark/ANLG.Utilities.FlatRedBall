namespace ANLG.Utilities.States.ModularStates;

public interface ISegmentModule
{
    bool TryHandleSegment();
    int TotalSegments { get; }
    int CurrentSegmentIndex { get; }
}