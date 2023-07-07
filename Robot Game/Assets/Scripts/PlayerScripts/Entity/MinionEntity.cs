using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.InputSystem;

public class MinionEntity : Entity
{
    public MinionData data;

    public Sprite icon;

    public List<OnHitPassive> onHitPassives;

    public Interactable currentInteractable;

    AnimatorOverrideController controller;

    //other
    public Transform hitboxes;

    public GameObject skillTree;

    private float baseAttackSpeed = 1f;

    //gameobject components
    protected Rigidbody2D rigBod;
    public Animator animator { get; protected set; }
    public Transform groundCheck;

    //other
    public string bufferedAction = "";
    public LayerMask whatIsGround;
    protected float groundedRadius = .1f;


    //movement variables
    protected int movementInputDirection;
    public bool canJump { get; protected set; }

    public static event Action effectUpdated;

    public virtual void Initialize(MinionData data)
    {
        this.data = data;
        onHitPassives = new List<OnHitPassive>();
        facingDirection = (int)transform.localScale.x;
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CreateStats();
        data.InitializePassives();
        controller = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = controller;
        EquipItemsFromData();
    }

    public override void Start()
    {
        // do all start stuff in initialize instead
    }

    public override void Update()
    {
        base.Update();
        if (bufferedAction != "" && animator.GetCurrentAnimatorStateInfo(0).IsTag("NeutralState"))
        {
            Invoke(bufferedAction, 0);
            bufferedAction = "";
        }
        Input();
    }

    public override void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Input()
    {
        animator.SetBool("Grounded", Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround));

        if (dead)
        {
            rigBod.velocity = Vector2.zero;
            return;
        }

        if (animator.GetBool("Grounded") && rigBod.velocity.y <= 0)
        {
            canJump = true;
            animator.SetBool("Jumping", false);
        }

        if (PlayerManager.instance.activeMinion == data)
        {
            // movement
            if (!(animator.GetCurrentAnimatorStateInfo(0).IsTag("stuckaction") || UIManager.instance.menuPreventingMovement))
            {
                movementInputDirection = (int)PlayerManager.instance.moveAction.ReadValue<Vector2>().x;
            }

            else
            {
                movementInputDirection = 0;
            }
        }
        else
        {
            movementInputDirection = 0;
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
        float targetSpeed = movementInputDirection * stats[EntityStatType.MoveSpeed].Value;
        float speedDiff = targetSpeed - rigBod.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stats[EntityStatType.MoveAcceleration].Value : stats[EntityStatType.MoveAcceleration].Value * 3;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, 1) * Mathf.Sign(speedDiff);
        rigBod.AddForce(movement * Vector2.right);

        // running anim
        animator.SetFloat("Running", Mathf.Abs(movementInputDirection));
        animator.SetFloat("Yvelocity", rigBod.velocity.y);
        animator.SetFloat("Xvelocity", (Mathf.Abs(rigBod.velocity.x) * .2f + .2f));
    }

    public virtual void PassInput(InputAction.CallbackContext context)
    {
        switch (context.action.name.ToLower())
        {
            case "jump":
                if (canJump)
                {
                    canJump = false;
                    rigBod.AddForce(Vector2.up * stats[EntityStatType.JumpForce].Value, ForceMode2D.Impulse);
                    animator.SetBool("Jumping", true);
                }
                break;
            case "basic attack":
                animator.SetFloat("AttackSpeedModifier", baseAttackSpeed * (1 + stats[EntityStatType.AttackSpeedBonus].Value / 100));
                Invoke("BasicAttack", 0);
                break;
            case "ability 1":
                animator.SetFloat("AttackSpeedModifier", baseAttackSpeed * (1 + stats[EntityStatType.AttackSpeedBonus].Value / 100));
                Invoke("Tornado", 0);
                break;
            default:
                break;
        }
    }

    //used to buffer actions
    protected virtual void AttemptAction(Animator animator, string animationName, string actionName)
    {
        //If there is no buffered action
        if ((bufferedAction == "" || bufferedAction == actionName) && animator.GetCurrentAnimatorStateInfo(0).IsTag("NeutralState"))
        {
            animator.Play(animationName, 0);
        }
        else
        {
            bufferedAction = actionName;
        }
    }

    public virtual void ToolAction()
    {

    }

    protected override void CreateStats()
    {
        base.CreateStats();
        // add listeners
        stats[EntityStatType.Gravity].statUpdated += () => { rigBod.gravityScale = stats[EntityStatType.Gravity].Value; };
        stats[EntityStatType.Gravity].Recalculate();
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

    protected void EquipItemsFromData()
    {
        foreach (Item item in new Item[] { data.weapon, data.tool})
        {
            if (item != null)
            {
                ((Equipable)item.GetItemFromDatabase()).Equip(ref stats);
            }
        }
    }

    /*
    public void OnDrawGizmos()
    {
        if (hitboxes.GetComponentsInChildren<BoxCollider2D>().Length > 0)
        {
            Gizmos.DrawWireCube(hitboxes.GetComponentsInChildren<BoxCollider2D>()[0].transform.position, hitboxes.GetComponentsInChildren<BoxCollider2D>()[0].size);
        }
    }
    */
}
