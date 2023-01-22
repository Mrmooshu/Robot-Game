using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class PlayerEntity : Entity
{
    [Header("Base Variant Info")]
    public string variantName = "Default";
    public GameObject skillTree;
    [Header("Base Variant Base Stats")]
    public int health;
    public int attackDamage;
    public int magicDamage;
    public int attackDefense;
    public int magicDefense;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;

    //core
    public PlayerCore core;
    public Interactable currentInteractable;


    //gameobject components
    protected Rigidbody2D rigBod { get; private set; }
    public Animator animator { get;  protected set; }
    public PlayerTool tool { get; private set; }
    public Transform groundCheck;

    //other
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    protected float groundedRadius = .1f;
    public Transform hitboxes;

    //movement variables
    private int movementInputDirection;
    public bool grounded { get; private set; }
    public bool canJump { get; private set; }



    public static event Action effectUpdated;

    public virtual void Initialize(PlayerCore core)
    {
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tool = transform.Find("Tool").GetComponent<PlayerTool>();
        this.core = core;
        CreateStats(new List<(StatType, float)> {
            (StatType.Health, health),
            (StatType.AttackDamage, attackDamage),
            (StatType.MagicDamage, magicDamage),
            (StatType.AttackDefense, attackDefense),
            (StatType.MagicDefense, magicDefense),
            (StatType.MoveSpeed, moveSpeed),
            (StatType.JumpForce, jumpForce),
            (StatType.Gravity, gravity)
        });
        ApplySkillStats();
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
            animator.SetBool("Jumping", false) ;
        }

        if (PlayerManager.instance.activeCore == core)
        {
            // movement
            if (!(animator.GetCurrentAnimatorStateInfo(0).IsTag("stuckaction") || UIManager.instance.menuPreventingMovement) && PlayerManager.instance.activeCore == core)
            {
                movementInputDirection = (int)Input.GetAxisRaw("Horizontal");

                if (Input.GetButtonDown("Jump") && canJump)
                {
                    canJump = false;
                    rigBod.AddForce(Vector2.up * stats[StatType.JumpForce].Value, ForceMode2D.Impulse);
                    animator.SetBool("Jumping", true);
                }
            }

            else
            {
                movementInputDirection = 0;
            }

            // attack
            if (Input.GetButton("Attack1") && grounded && core.currentBody.weapon != null)
            {
                animator.SetTrigger("Basic");
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
            // turn around
            if ((facingDirection > 0 && movementInputDirection < 0) || (facingDirection < 0 && movementInputDirection > 0))
            {
                Flip();
            }
        }
        // movement
        float targetSpeed = movementInputDirection * stats[StatType.MoveSpeed].Value;
        float speedDiff = targetSpeed - rigBod.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stats[StatType.MoveSpeed].Value*.5f : stats[StatType.MoveSpeed].Value*2;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1) * Mathf.Sign(speedDiff);
        rigBod.AddForce(movement * Vector2.right);

        // player running anim
        animator.SetFloat("Running", Mathf.Abs(movementInputDirection));
        animator.SetFloat("Yvelocity", rigBod.velocity.y);
    }

    public abstract void ToolAction();

    public abstract void BasicAttack();

    protected override void CreateStats(List<(StatType, float)> statList = null)
    {
        base.CreateStats(statList);

        // add listeners
        stats[StatType.Gravity].statUpdated += () => { rigBod.gravityScale = stats[StatType.Gravity].Value; };
    }

    protected virtual void ApplySkillStats()
    {
        Debug.Log(stats[StatType.Health].Value);
        foreach (KeyValuePair<string,int> skill in core.currentBody.skills)
        {
            foreach (PassiveStat passive in skillTree.GetComponentsInChildren<PassiveStat>())
            {
                passive.InitializePassive(this);
                Debug.Log(stats[StatType.Health].Value);
            }
        }
    }

    public void InvokeEffectUpdate()
    {
        effectUpdated?.Invoke();
    }
}
