using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterEntity : Entity
{
    public Transform groundCheck;
    public LayerMask whatIsGround;
    protected float groundedRadius = .1f;
    protected float wallCheckLength = .1f;
    public float spawnX;
    public int maxDistanceFromSpawn = 0;
    public int movementDirection;
    public bool canJump;

    public BehaviourNode brain;

    public override void Start()
    {
        base.Start();
        spawnX = transform.position.x;

        CreateBrain();
    }

    public override void Update()
    {
        base.Update();
        MovementChecks();
    }

    public override void FixedUpdate()
    {
        Movement();
    }

    protected virtual void MovementChecks()
    {
        animator.SetBool("Grounded", Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround));

        if (dead)
        {
            rigBod.velocity = Vector2.zero;
            return;
        }
        if (animator.GetFloat("Hitstun") > 0)
        {
            return;
        }

        if (animator.GetBool("Grounded") && rigBod.velocity.y <= 0)
        {
            canJump = true;
        }

        if ((facingDirection > 0 && movementDirection < 0) || (facingDirection < 0 && movementDirection > 0))
        {
            Flip();
        }

        // running anim
        animator.SetFloat("Running", Mathf.Abs(movementDirection));
        animator.SetFloat("Yvelocity", rigBod.velocity.y);
    }

    protected virtual void Movement()
    {
        float targetSpeed = movementDirection * stats[EntityStatType.MoveSpeed].Value;
        float speedDiff = targetSpeed - rigBod.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stats[EntityStatType.MoveSpeed].Value * 1f : stats[EntityStatType.MoveSpeed].Value * 2;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1) * Mathf.Sign(speedDiff);
        rigBod.AddForce(movement * Vector2.right);
    }

    //Override this with an assignment to brain and call base
    protected virtual void CreateBrain()
    {
        gameObject.AddComponent<BehaviourTree>();
    }

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        rigBod.simulated = false;
        enabled = false;
    }
}
