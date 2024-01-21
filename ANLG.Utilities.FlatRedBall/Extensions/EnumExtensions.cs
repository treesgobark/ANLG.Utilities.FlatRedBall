using ANLG.Utilities.FlatRedBall.Constants;

namespace ANLG.Utilities.FlatRedBall.Extensions;

public static class EnumExtensions
{
    public static int GetSign(this FourDirections direction)
    {
        return direction switch
        {
            FourDirections.Right => 1,
            FourDirections.Left  => -1,
            FourDirections.Up    => 1,
            FourDirections.Down  => -1,
            _ => throw new ArgumentException($"Invalid enum value : {direction}"),
        };
    }
}
