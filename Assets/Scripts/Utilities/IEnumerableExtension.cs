using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtension
{
    public static T Random<T>(this IEnumerable<T> vs)
    {
        return vs.ElementAt(UnityEngine.Random.Range(0, vs.Count()));
    }
}
