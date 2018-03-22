using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static T Random<T>(this T[] array)
    {
        int index = UnityEngine.Random.Range(0, array.Length);
        return array[index];
    }

    public static T Random<T>(this List<T> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }
}
