using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public PlayerCore core;
    public InteractableEntity currentInteractable;

    protected bool cantMove = false;
    private int movementInputDirection;
    private bool grounded, canJump;
    private float groundedRadius = .1f;
    protected Rigidbody2D rigBod;
    protected Animator animator;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float movementSpeed, jumpForce;
    [SerializeField] public LayerMask whatIsGround;

    [Header("Player Specific Stats")]
    [SerializeField] public Weapon weaponSlot;

    public void Initialize(PlayerCore core, float movementSpeed, float jumpForce)
    {
        this.core = core;
        this.movementSpeed = movementSpeed;
        this.jumpForce = jumpForce;
    }

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

    public void TakeControl()
    {
        PlayerManager.instance.activeCore = core;
        PlayerManager.instance.mainCam.GetComponent<CameraFollow>().followTransform = transform;
    }

    private void PlayerInput()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        
        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
        }

        // only when this player is in control
        if (PlayerManager.instance.activeCore == core)
        {
            if (!cantMove)
            {
                movementInputDirection = (int)Input.GetAxisRaw("Horizontal");

                if (Input.GetButtonDown("Jump") && canJump)
                {
                    canJump = false;
                    rigBod.velocity = new Vector2(rigBod.velocity.x, jumpForce);
                }
            }
            else
            {
                movementInputDirection = 0;
            }
            // player running anim
            animator.SetInteger("Run", movementInputDirection);
            // weapon running anim
            transform.GetChild(0).GetComponent<Animator>().SetInteger("Run", movementInputDirection);

            // left click is down
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    // Player Control Change
                    if (hit.collider.gameObject.GetComponent<PlayerEntity>() && hit.collider.gameObject != gameObject && Input.GetMouseButtonDown(0))
                    {
                        PlayerManager.instance.ControlThisPlayer(hit.collider.gameObject.GetComponent<PlayerEntity>());
                    }
                    // Item Pick Up
                    if (hit.collider.gameObject.GetComponent<ItemObject>())
                    {
                        if (core.inventory.Add(hit.collider.gameObject.GetComponent<ItemObject>().item))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        else
                        {
                            Debug.Log("full inventory");
                        }
                    }
                }
            }
        }
    }

    private void Movement()
    {
        if (PlayerManager.instance.activeCore == core)
        {
            if ((facingDirection > 0 && movementInputDirection < 0) || (facingDirection < 0 && movementInputDirection > 0))
            {
                Flip();
            }
        }
        rigBod.velocity = new Vector2(movementSpeed * movementInputDirection, rigBod.velocity.y);
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
