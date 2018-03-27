using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static T Random<T>(this T[] array)
    {
        if (array.Length == 0) Debug.LogError("Array is empty! ");

        int index = UnityEngine.Random.Range(0, array.Length);
        return array[index];
    }

    public static T Random<T>(this List<T> list)
    {
        if (list.Count == 0) Debug.LogError("List is empty! ");

        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }

    public static T ReturnAndRemoveRandom<T>(this List<T> list)
    {
        if (list.Count == 0) Debug.LogError("List is empty! ");

        int index = UnityEngine.Random.Range(0, list.Count);
        T safe = list[index];
        list.Remove(safe);
        return safe;
    }

    public static void ForEach<T>(this T[] array, Action<T> action)
    {
        foreach (var item in array)
            action(item);
    }
}

[System.Serializable]
public class MinMax
{
    public float min;
    public float max;

    public MinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
