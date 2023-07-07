using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField]private List<EntitiyStatStruct> statListInspector;

    public int facingDirection { get; protected set; }
    public bool dead = false;
    public bool stunned = false;
    public LayerMask whatIsEnemy;

    public Dictionary<EntityStatType, Stat> stats;
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
        stats = new Dictionary<EntityStatType, Stat>();
        effects = new Dictionary<int, Effect>();

        foreach (EntitiyStatStruct stat in statListInspector)
        {
            if (stat.type == EntityStatType.Health || stat.type == EntityStatType.Mana)
            {
                stats.Add(stat.type, new ResourceStat(stat.value));
            }
            else
            {
                stats.Add(stat.type, new Stat(stat.value));
            }
        }

        if (stats.ContainsKey(EntityStatType.Health))
        {
            ((ResourceStat)stats[EntityStatType.Health]).currentValueUpdated += CheckToDie;
        }
    }

    private void CheckToDie()
    {
        if (((ResourceStat)stats[EntityStatType.Health]).CurrentValue <= 0)
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