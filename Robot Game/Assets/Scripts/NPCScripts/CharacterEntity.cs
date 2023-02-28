using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : Entity
{
    protected Rigidbody2D rigBod;
    protected Animator animator;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform jumpCheck;
    public LayerMask whatIsGround;
    protected float groundedRadius = .1f;
    protected float wallCheckLength = .1f;
    public int walkTimer = 0;
    public int waitTimer = 0;
    public float spawnX;
    public int maxDistanceFromSpawn = 0;
    public int movementDirection;
    public bool grounded, canJump;

    public override void Start()
    {
        base.Start();
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnX = transform.position.x;
    }

    public override void Update()
    {
        base.Update();
        if (walkTimer > 0)
        {
            movementDirection = facingDirection;
        }
        else
        {
            movementDirection = 0;
        }
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
        if (stunned)
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
        float targetSpeed = movementDirection * stats[StatType.MoveSpeed].Value;
        float speedDiff = targetSpeed - rigBod.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stats[StatType.MoveSpeed].Value * 1f : stats[StatType.MoveSpeed].Value * 2;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1) * Mathf.Sign(speedDiff);
        rigBod.AddForce(movement * Vector2.right);

        // running anim
        animator.SetFloat("Running", Mathf.Abs(movementDirection));
    }

    public void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rigBod.velocity = new Vector2(rigBod.velocity.x, stats[StatType.JumpForce].Value);
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * .7f));
        Gizmos.DrawLine(jumpCheck.position, jumpCheck.position + (Vector3)(Vector2.right * facingDirection * .7f));
    }
}
