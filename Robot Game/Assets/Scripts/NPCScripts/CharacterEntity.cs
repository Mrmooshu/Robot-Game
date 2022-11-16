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
    public float movementSpeed, jumpForce;
    public LayerMask whatIsGround;
    protected float groundedRadius = .1f;
    protected float wallCheckLength = .1f;
    public float walkSpeed = 1;
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

        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
        }

        if ((facingDirection > 0 && movementDirection < 0) || (facingDirection < 0 && movementDirection > 0))
        {
            Flip();
        }
        if (walkTimer > 0)
        {
            rigBod.velocity = new Vector2(walkSpeed * movementDirection, rigBod.velocity.y);
        }
        else
        {
            rigBod.velocity = new Vector2(movementSpeed * movementDirection, rigBod.velocity.y);
        }

        // running anim
        animator.SetInteger("Run", movementDirection);
    }

    public void Jump()
    {
        if (canJump)
        {
            canJump = false;
            rigBod.velocity = new Vector2(rigBod.velocity.x, jumpForce);
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * .7f));
        Gizmos.DrawLine(jumpCheck.position, jumpCheck.position + (Vector3)(Vector2.right * facingDirection * .7f));
    }
}
