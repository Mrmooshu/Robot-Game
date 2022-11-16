using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int facingDirection { get; protected set; }

    public Dictionary<StatType, Stat> stats;
    public Dictionary<int,Effect> effects;

    public virtual void Start()
    {
        facingDirection = (int)transform.localScale.x;
    }

    public virtual void Update()
    {


    }

    public virtual void FixedUpdate()
    {

    }

    public void Flip()
    {
        facingDirection *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetDirection(int direction)
    {
        Vector3 theScale = transform.localScale;
        if (direction >= 0)
        {
            facingDirection = 1;
            theScale.x = 1;
        }
        else
        {
            facingDirection = -1;
            theScale.x = -1;
        }
        transform.localScale = theScale;
    }

    protected virtual void CreateStats(List<(StatType, float)> statList = null)
    {
        stats = new Dictionary<StatType, Stat>();
        effects = new Dictionary<int, Effect>();

        foreach ((StatType, float) stat in statList)
        {
            stats.Add(stat.Item1, new Stat(stat.Item2));
        }
    }
}