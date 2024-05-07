using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public static event Action levelup;

    public MinionData data;

    [SerializeField]private int level;
    [SerializeField]private float exp;
    public int Level { get { return level; } private set { level = value; } }
    public float Exp { get { return exp; } private set { exp = value; } }

    public LevelData(MinionData data = null)
    {
        Level = 1;
        Exp = 0;
        this.data = data;
    }

    public void AddExp(float xp)
    {
        Exp += xp;
        while (Exp >= GetExpForNextLevel(Level))
        {
            Level++;
            levelup.Invoke();
        }
        // 10% of function exp is gained as main exp
        if (data != null)
        {
            data.Level.AddExp(xp * .1f);
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
