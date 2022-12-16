namespace ANLG.Utilities.FlatRedBall.Extensions;

///
public static class FloatExtensions
{
    /// <summary>
    /// Determines the difference in signs between two floats.
    /// </summary>
    /// <returns>1 if the signs are the same, -1 otherwise.</returns>
    public static float SignDifference(this float input1, float input2)
    {
        if (input1 < 0)
        {
            return input2 < 0 ? 1 : -1;
        }
        return input2 > 0 ? 1 : -1;
    }
}