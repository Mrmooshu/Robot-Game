using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerEntity : Entity
{
    public PlayerCore core;
    public Interactable currentInteractable;

    protected Rigidbody2D rigBod;
    public Animator upperAnimator { get;  protected set; }
    public Animator lowerAnimator { get; protected set; }
    public Animator frontAnimator { get; protected set; }
    public Animator backAnimator { get; protected set; }
    public PlayerTool tool;
    public PlayerWeapon weaponFront;
    public PlayerWeapon weaponBack;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    protected float groundedRadius = .1f;

    private int movementInputDirection;
    public bool grounded, canJump;

    public static event Action effectUpdated;

    public void Initialize(PlayerCore core)
    {
        rigBod = GetComponent<Rigidbody2D>();
        upperAnimator = transform.Find("UpperBody").GetComponent<Animator>();
        lowerAnimator = transform.Find("LowerBody").GetComponent<Animator>();
        frontAnimator = transform.Find("FrontArm").GetComponent<Animator>();
        backAnimator = transform.Find("BackArm").GetComponent<Animator>();
        tool = transform.Find("Tool").GetComponent<PlayerTool>();
        weaponFront = transform.Find("WeaponFront").GetComponent<PlayerWeapon>();
        weaponBack = transform.Find("WeaponBack").GetComponent<PlayerWeapon>();
        this.core = core;
        CreateStats(new List<(StatType, float)> {
            (StatType.MoveSpeed, Database.GetVariant(core.currentBody.variantName).moveSpeed),
            (StatType.JumpForce, Database.GetVariant(core.currentBody.variantName).jumpForce),
            (StatType.Gravity, Database.GetVariant(core.currentBody.variantName).gravity)
        });
    }

    public override void Update()
    {
        base.Update();
        PlayerInput();
    }

    public override void  FixedUpdate()
    {
        Movement();
    }

    private void PlayerInput()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        
        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
        }

        if (PlayerManager.instance.activeCore == core)
        {
            // movement
            if (!(tool.toolAnimator.GetBool("Skilling") || UIManager.instance.menuPreventingMovement) && PlayerManager.instance.activeCore == core)
            {
                movementInputDirection = (int)Input.GetAxisRaw("Horizontal");

                if (Input.GetButtonDown("Jump") && canJump)
                {
                    canJump = false;
                    rigBod.velocity = new Vector2(rigBod.velocity.x, stats[StatType.JumpForce].Value);
                }
            }

            else
            {
                movementInputDirection = 0;
            }

            // attack
            if (Input.GetButtonDown("Attack1") && core.currentBody.weapon != null)
            {
                weaponFront.UpdateAnimators();
                weaponBack.UpdateAnimators();
                if (weaponFront.weaponAnimator.runtimeAnimatorController != null)
                {
                    weaponFront.weaponAnimator.SetTrigger("Attack");
                }
                if (weaponBack.weaponAnimator.runtimeAnimatorController != null)
                {
                    weaponBack.weaponAnimator.SetTrigger("Attack");
                }

            }

            // left click is down
            if (Input.GetMouseButton(0) && PlayerManager.instance.activeCore == core)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    // Player Control Change (disabled and replaced with core select menu)
                    /*
                    if (hit.collider.gameObject.GetComponent<PlayerEntity>() && hit.collider.gameObject != gameObject && Input.GetMouseButtonDown(0))
                    {
                        PlayerManager.instance.SetActiveCore(hit.collider.gameObject.GetComponent<PlayerEntity>().core);
                    }
                    */
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
        else
        {
            movementInputDirection = 0;
        }
    }

    protected  void Movement()
    {
        if (PlayerManager.instance.activeCore == core)
        {
            if ((facingDirection > 0 && movementInputDirection < 0) || (facingDirection < 0 && movementInputDirection > 0))
            {
                Flip();
            }
        }
        rigBod.velocity = new Vector2(stats[StatType.MoveSpeed].Value * movementInputDirection, rigBod.velocity.y);

        // player running anim
        upperAnimator.SetInteger("Run", movementInputDirection);
    }

    public virtual void ToolAction()
    {

    }

    protected override void CreateStats(List<(StatType, float)> statList = null)
    {
        base.CreateStats(statList);

        // add listeners
        stats[StatType.Gravity].statUpdated += () => { rigBod.gravityScale = stats[StatType.Gravity].Value; };
    }

    public void InvokeEffectUpdate()
    {
        effectUpdated?.Invoke();
    }
}
