using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MinionEntity : Entity
{
    public MinionData data;

    public List<IOnHit> onHitPassives;

    public Interactable currentInteractable;

    AnimatorOverrideController controller;

    //other
    public Transform hitboxes;

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

    public virtual void Initialize(MinionData data)
    {
        this.data = data;
        onHitPassives = new List<IOnHit>();
        facingDirection = (int)transform.localScale.x;
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CreateStats();
        ApplySkills();
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

        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
            animator.SetBool("Jumping", false);
        }

        if (PlayerManager.instance.activeMinion == data)
        {
            // movement
            if (!(animator.GetCurrentAnimatorStateInfo(0).IsTag("stuckaction") || UIManager.instance.menuPreventingMovement) && PlayerManager.instance.activeMinion == data)
            {
                movementInputDirection = (int)UnityEngine.Input.GetAxisRaw("Horizontal");

                if (UnityEngine.Input.GetButtonDown("Jump") && canJump)
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
            if (UnityEngine.Input.GetButton("Attack1"))
            {
                var baseAttackSpeed = 1f;
                if (data.weapon != null)
                {
                    baseAttackSpeed = ((Weapon)Database.GetItem(data.weapon.itemID)).baseAttackSpeed;
                }
                animator.SetFloat("AttackSpeedModifier", baseAttackSpeed + baseAttackSpeed * stats[StatType.AttackSpeedBonus].Value);
                animator.SetTrigger("Basic");

            }

            // left click is down
            if (UnityEngine.Input.GetMouseButton(0) && PlayerManager.instance.activeMinion == data)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
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
                        if (data.inventory.Add(hit.collider.gameObject.GetComponent<ItemObject>().item))
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

    public virtual void BasicAttack()
    {
        animator.ResetTrigger("Basic");
        if (data.weapon != null)
        {
            ((Weapon)Database.GetItem(data.weapon.itemID)).BasicAttack(this);
        }
        else
        {
            DefaultBasic();
        }



        //Vector2 knockback = new Vector2(100 * facingDirection, 100);
        //DamageScript.Attack(new DamageScript.attackData(this, new DamageScript.damageData(stats[StatType.AttackDamage].Value, DamageScript.damageType.physical), true, knockback, .5f), hitboxes, whatIsEnemy, this);
        //rigBod.AddForce(new Vector2(stats[StatType.MoveSpeed].Value * facingDirection, 1.2f), ForceMode2D.Impulse);
        //kill switch (uncomment to kill urself)
        //((ResourceStat)stats[StatType.Health]).CurrentValue -= 1000;
    }

    protected virtual void DefaultBasic()
    {

    }

    public virtual void ToolAction()
    {

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

    public void OnDrawGizmos()
    {
        if (hitboxes.GetComponentsInChildren<BoxCollider2D>().Length > 0)
        {
            Gizmos.DrawWireCube(hitboxes.GetComponentsInChildren<BoxCollider2D>()[0].transform.position, hitboxes.GetComponentsInChildren<BoxCollider2D>()[0].size);
        }
    }
}
