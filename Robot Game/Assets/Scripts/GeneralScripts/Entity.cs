using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [SerializeField]private List<EntitiyStatStruct> statListInspector;

    public Rigidbody2D rigBod { get; protected set; }
    public Animator animator { get; protected set; }

    public HitboxScript hitboxes;

    public int facingDirection { get; protected set; }
    public bool dead = false;
    public LayerMask whatIsEnemy;

    public Dictionary<EntityStatType, Stat> stats;
    public Dictionary<int,Effect> effects;

    public virtual void Start()
    {
        facingDirection = (int)transform.localScale.x;
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CreateStats();
    }

    public virtual void Update()
    {
        HitstunTick();
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
            if (stat.type == EntityStatType.health.ToString() || stat.type == EntityStatType.mana.ToString())
            {
                stats.Add((EntityStatType)Enum.Parse(typeof(EntityStatType), stat.type.ToLower()), new ResourceStat(stat.value));
            }
            else
            {
                stats.Add((EntityStatType)Enum.Parse(typeof(EntityStatType), stat.type.ToLower()), new Stat(stat.value));
            }
        }

        if (stats.ContainsKey(EntityStatType.health))
        {
            ((ResourceStat)stats[EntityStatType.health]).currentValueUpdated += CheckToDie;
        }
    }

    private void CheckToDie()
    {
        if (((ResourceStat)stats[EntityStatType.health]).CurrentValue <= 0)
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

    private void HitstunTick()
    {
        if (animator.GetFloat("Hitstun") > 0)
        {
            animator.SetFloat("Hitstun", animator.GetFloat("Hitstun") - Time.deltaTime);
        }
    }
}