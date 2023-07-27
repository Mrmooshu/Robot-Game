using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterEntity : Entity
{
    public Rigidbody2D rigBod { get; protected set; }
    public Animator animator{ get; protected set; }
    public Transform groundCheck;
    public LayerMask whatIsGround;
    protected float groundedRadius = .1f;
    protected float wallCheckLength = .1f;
    public float spawnX;
    public int maxDistanceFromSpawn = 0;
    public int movementDirection;
    public bool grounded, canJump;

    public BehaviourNode brain;

    public override void Start()
    {
        base.Start();
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnX = transform.position.x;

        CreateBrain();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        if (dead)
        {
            rigBod.velocity = Vector2.zero;
            return;
        }
        if (hitStunDuration > 0)
        {
            return;
        }

        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
        }

        if ((facingDirection > 0 && movementDirection < 0) || (facingDirection < 0 && movementDirection > 0))
        {
            Flip();
        }
        float targetSpeed = movementDirection * stats[EntityStatType.MoveSpeed].Value;
        float speedDiff = targetSpeed - rigBod.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stats[EntityStatType.MoveSpeed].Value * 1f : stats[EntityStatType.MoveSpeed].Value * 2;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1) * Mathf.Sign(speedDiff);
        rigBod.AddForce(movement * Vector2.right);

        // running anim
        animator.SetFloat("Running", Mathf.Abs(movementDirection));
    }

    //Override this with an assignment to brain and call base
    protected virtual void CreateBrain()
    {
        gameObject.AddComponent<BehaviourTree>();
    }
}
