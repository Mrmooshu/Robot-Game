using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitEntity : Entity
{
    public GameObject skillTree;

    //gameobject components
    protected Rigidbody2D rigBod;
    public Animator animator { get; protected set; }
    public Transform groundCheck;

    //other
    public LayerMask whatIsGround;
    protected float groundedRadius = .1f;

    //movement variables
    protected int movementInputDirection;
    public bool grounded { get; protected set; }
    public bool canJump { get; protected set; }

    public static event Action effectUpdated;



    public virtual void Initialize()
    {
        facingDirection = (int)transform.localScale.x;
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CreateStats();
        ApplySkills();
    }

    public override void Start()
    {
        // do all start stuff in initialize instead
    }

    public override void Update()
    {
        base.Update();
        Input();
    }

    public override void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Input()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        if (dead)
        {
            rigBod.velocity = Vector2.zero;
            return;
        }
    }

    protected virtual void Movement()
    {
        // turn around
        if ((facingDirection > 0 && movementInputDirection < 0) || (facingDirection < 0 && movementInputDirection > 0))
        {
            Flip();
        }

        // movement
        float targetSpeed = movementInputDirection * stats[StatType.MoveSpeed].Value;
        float speedDiff = targetSpeed - rigBod.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stats[StatType.MoveAcceleration].Value : stats[StatType.MoveAcceleration].Value * 3;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1) * Mathf.Sign(speedDiff);
        rigBod.AddForce(movement * Vector2.right);

        // running anim
        animator.SetFloat("Running", Mathf.Abs(movementInputDirection));
        animator.SetFloat("Yvelocity", rigBod.velocity.y);
        animator.SetFloat("Xvelocity", (Mathf.Abs(rigBod.velocity.x) * .2f + .2f));
    }

    protected virtual void ApplySkills()
    {
        //apply skills for skill tree
        foreach (Passive passive in skillTree.GetComponentsInChildren<Passive>())
        {
            passive.InitializePassive(this);
        }
    }

    protected override void CreateStats()
    {
        base.CreateStats();
        // add listeners
        stats[StatType.Gravity].statUpdated += () => { rigBod.gravityScale = stats[StatType.Gravity].Value; };
    }

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
    }

    public void InvokeEffectUpdate()
    {
        effectUpdated?.Invoke();
    }
}
