using System.Numerics;

namespace ANLG.Utilities.Core.Extensions;

public static class RandomExtensions
{
    /// <summary>
    /// Randomly returns 1 or -1.
    /// </summary>
    public static int NextSign(this Random input)
    {
        return input.Next(2) * 2 - 1;
    }
    
    /// <summary>
    /// Randomly returns 1 or -1.
    /// </summary>
    public static bool NextBool(this Random input)
    {
        return input.Next(2) != 0;
    }

    /// <summary>
    /// Returns a value greater than or equal to <c>value * (1 - tolerance)</c> and less than <c>value * (1 + tolerance)</c>
    /// </summary>
    public static float RandomizeByTolerance(this Random random, float value, float tolerance)
    {
        return float.Lerp(value * (1 - tolerance), value * (1 + tolerance), random.NextSingle());
    }
}
