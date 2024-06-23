using ANLG.Utilities.Core.NonStaticUtilities;
using FlatRedBall;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public class FrbTimeManager : ITimeManager
{
    public TimeSpan GameTimeSinceLastFrame => TimeSpan.FromSeconds(TimeManager.SecondDifference);
}