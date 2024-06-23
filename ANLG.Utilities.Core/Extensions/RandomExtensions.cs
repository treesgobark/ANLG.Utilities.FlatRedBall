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
}
