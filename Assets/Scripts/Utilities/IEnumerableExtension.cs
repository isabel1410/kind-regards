using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtension
{
    /// <summary>
    /// Select a random item using UnityEngine.Random from an IEnumerable
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    /// <param name="vs">The IEnumerable</param>
    /// <returns></returns>
    public static T Random<T>(this IEnumerable<T> vs)
    {
        return vs.ElementAt(UnityEngine.Random.Range(0, vs.Count()));
    }
}
