using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    private int movementInputDirection;
    private bool grounded, canJump;
    private float groundedRadius = .1f;
    protected Rigidbody2D rigBod;
    protected Animator animator;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private float movementSpeed, jumpForce;
    [SerializeField] public LayerMask whatIsGround;

    public override void Start()
    {
        base.Start();
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        PlayerInput();
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void PlayerInput()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        movementInputDirection = (int)Input.GetAxisRaw("Horizontal");

        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
        }

        animator.SetInteger("Run", movementInputDirection);

        if (Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            rigBod.velocity = new Vector2(rigBod.velocity.x, jumpForce);
        }
    }

    private void Movement()
    {
        if ((facingDirection > 0 && movementInputDirection < 0) || (facingDirection < 0 && movementInputDirection > 0))
        {
            Flip();
        }

        rigBod.velocity = new Vector2(movementSpeed * movementInputDirection, rigBod.velocity.y);
    }
}
