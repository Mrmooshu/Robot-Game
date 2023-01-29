using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Serializable]
    public struct StatStruct
    {
        public StatType type;
        public float value;
    }

    [SerializeField]private List<StatStruct> statListInspector;

    public int facingDirection { get; protected set; }
    public bool dead = false;

    public Dictionary<StatType, Stat> stats;
    public Dictionary<int,Effect> effects;

    public virtual void Start()
    {
        facingDirection = (int)transform.localScale.x;
        CreateStats();
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

    protected virtual void CreateStats()
    {
        stats = new Dictionary<StatType, Stat>();
        effects = new Dictionary<int, Effect>();

        foreach (StatStruct stat in statListInspector)
        {
            if (stat.type == StatType.Health || stat.type == StatType.Mana)
            {
                stats.Add(stat.type, new ResourceStat(stat.value));
            }
            else
            {
                stats.Add(stat.type, new Stat(stat.value));
            }
        }

        if (stats.ContainsKey(StatType.Health))
        {
            ((ResourceStat)stats[StatType.Health]).currentValueUpdated += CheckToDie;
        }
    }

    private void CheckToDie()
    {
        if (((ResourceStat)stats[StatType.Health]).CurrentValue <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        dead = true;
        if (GetComponent<DropTableRoller>())
        {
            GetComponent<DropTableRoller>().RollDrop();
        }
    }
}