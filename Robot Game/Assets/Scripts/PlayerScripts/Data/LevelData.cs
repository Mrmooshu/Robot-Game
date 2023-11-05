using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public static event Action levelup;

    public int level { get; private set; }
    public float exp { get; private set; }

    public LevelData()
    {
        level = 1;
        exp = 0;
    }

    public void AddExp(float xp)
    {
        exp += xp;
        while (exp >= GetExpForNextLevel(level))
        {
            level++;
            levelup.Invoke();
        }
    }

    public static float GetExpForNextLevel(int level)
    {
        var counter = 0f;
        for (int i = 1; i <= level; i++)
        {
            counter += Mathf.Floor(i + (300 * Mathf.Pow(2, i/7f)));
        }
        return counter/4;
    }
}
