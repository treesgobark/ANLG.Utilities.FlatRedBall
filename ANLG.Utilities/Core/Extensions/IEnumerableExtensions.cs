namespace ANLG.Utilities.Core;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Returns a random element from the list
    /// </summary>
    public static T ChooseRandom<T>(this IReadOnlyList<T> list, Random? instance = null)
    {
        if (instance is null)
        {
            instance = Random.Shared;
        }

        int index = instance.Next(list.Count);

        return list[index];
    }
}