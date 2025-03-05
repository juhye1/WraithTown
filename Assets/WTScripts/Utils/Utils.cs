using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void Shuffle<T>(T[] array)
    {
        System.Random random = new System.Random();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
    public static void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public static bool GetRandomNum(float percent)
    {
        System.Random random = new System.Random();
        int val = random.Next(0, 101);
        if(val < percent * 100)
        {
            return true;
        }
        return false;
    }

    public static int GetRandomNum(int max)
    {
        System.Random random = new System.Random();
        int val = random.Next(0, max);
        return val;
    }
}