using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public static class Extensions
{
    public static T GetRandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static void WaitAndDo(this MonoBehaviour mono, float seconds, Action action)
    {
        if (action != null)
        {
            if (seconds > 0)
            {
                mono.StartCoroutine(WaitAndDoCoroutine(seconds, action));
            }
            else
            {
                action();
            } 
        }
    }

    private static IEnumerator WaitAndDoCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);

        try
        {
            action();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
