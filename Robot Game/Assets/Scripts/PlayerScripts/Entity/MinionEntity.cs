using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public abstract class MinionEntity : Entity
{

    public MinionData data { get; protected set; }

    public Sprite icon;

    public SpriteRenderer weapon;

    [NonSerialized] public List<Passive> passives;

    public List<OnHitPassive> onHitPassives;

    public Interactable currentInteractable;

    AnimatorOverrideController controller;

    FollowContoller followController;

    //other

    public GameObject skillTree;

    public float baseAttackSpeed { get; protected set; } = 1f;

    //gameobject components
    public Transform groundCheck;

    //other
    public bool moveLocked = false;
    public bool jumpLocked = false;
    public Action bufferedAction = null;
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
        followController = GetComponent<FollowContoller>();
        groundCheck = transform.Find("GroundCheck");
        CreateStats();
        InitializePassives();
        data.functions.Where(x => x != null).ToList().ForEach(x => x.InitializePassives());
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
        if (bufferedAction != null && animator.GetCurrentAnimatorStateInfo(0).IsTag("NeutralState"))
        {
            bufferedAction();
            bufferedAction = null;
        }
        ActiveCoolDownTick();
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
            if (!(animator.GetBool("Following") || moveLocked || UIManager.instance.menuPreventingMovement))
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
        float targetSpeed = movementInputDirection * stats[EntityStatType.movespeed].Value;
        float speedDiff = targetSpeed - rigBod.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? stats[EntityStatType.moveacceleration].Value : stats[EntityStatType.moveacceleration].Value * 3;
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
            case "test":
                RemoteMenuToggle.ToggleThis("RebirthToggle");
                Debug.Log("test");
                break;
            case "jump":
                Jump();
                break;
            case "basic attack":
                animator.SetFloat("AttackSpeedModifier", baseAttackSpeed * (1 + stats[EntityStatType.attackspeedbonus].Value / 100));
                switch (data.style)
                {
                    case MinionData.CombatStyle.Melee:
                        MeleeBasic();
                        break;
                    case MinionData.CombatStyle.Range:
                        RangeBasic();
                        break;
                    case MinionData.CombatStyle.Magic:
                        MagicBasic();
                        break;
                }
                break;
            case "ability 1":
                ActivateAbility(0);
                break;
            case "ability 2":
                ActivateAbility(1);
                break;
            case "ability 3":
                ActivateAbility(2);
                break;
            case "ability 4":
                ActivateAbility(3);
                break;
            case "ability 5":
                ActivateAbility(4);
                break;
            case "ability 6":
                ActivateAbility(5);
                break;
            default:
                break;
        }

        void ActivateAbility(int index)
        {
            if (AbilitySlot.instances[index].iconGO != null)
            {
                AbilitySlot.instances[index].GetComponentInChildren<ActiveAbilityIcon>().Activate();
            }
        }
    }

    protected void Jump()
    {
        if (canJump && !jumpLocked && !animator.GetBool("Following"))
        {
            canJump = false;
            rigBod.AddForce(Vector2.up * stats[EntityStatType.jumpforce].Value, ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
        }
    }

    //used to buffer actions
    protected virtual bool AttemptAction(Action action = null)
    {
        //If there is no buffered action
        if ((bufferedAction == null) && animator.GetCurrentAnimatorStateInfo(0).IsTag("NeutralState"))
        {
            action();
            return true;
        }
        else
        {
            bufferedAction = action;
            return false;
        }
    }

    protected override void CreateStats()
    {
        base.CreateStats();
        foreach (ClassFunction function in data.functions)
        {
            if (function != null)
            {
                function.AddStats();
            }

        }
        // add listeners
        //stats[EntityStatType.Gravity].statUpdated += () => { rigBod.gravityScale = stats[EntityStatType.Gravity].Value; };
        //stats[EntityStatType.Gravity].Recalculate();
    }

    public virtual void InitializePassives()
    {
        passives = new List<Passive>();
        passives.Add(new HealthRegenPassive(this));
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
        foreach (Item item in new Item[] {}.Concat(data.artifacts).Concat(data.functions.Select(x => x == null ? null : x.equipItem).ToList()))
        {
            if (item != null)
            {
                ((Equipable)item.GetItemFromDatabase()).Equip(ref stats);
            }
        }
    }

    public void CalculateASForAnimator()
    {
        animator.SetFloat("AttackSpeedModifier", baseAttackSpeed * (1 + stats[EntityStatType.attackspeedbonus].Value / 100));
    }

    protected void StartActiveCooldown(ActiveAbilityIcon active)
    {
        var index = Array.FindIndex(PlayerManager.instance.activeMinion.ActiveAbilities, x => x.name == active.activeAbility);
        var cdrAppliedCD = Database.GetActiveAbility(active.activeAbility).CoolDown;
        data.ActiveAbilities[index].cooldown = cdrAppliedCD;
        active.ResetStage();
    }

    private void ActiveCoolDownTick()
    {
        for (int i = 0; i < data.ActiveAbilities.Length; i++)
        {
            if (data.ActiveAbilities[i].cooldown > 0)
            {
                data.ActiveAbilities[i].cooldown -= Time.deltaTime;
            }
            else
            {
                data.ActiveAbilities[i].cooldown = 0;
            }
        }
    }

    public void InteractAction()
    {
        if (currentInteractable != null)
        {
            currentInteractable.InteractAction(data);
        }
        else
        {
            animator.Play("Idle",0);
        }
    }

    public abstract void MeleeBasic();

    public abstract void RangeBasic();

    public abstract void MagicBasic();

    // Active Abilities
    protected void RoamingCancel(ActiveAbilityIcon active)
    {
        for (int i = 0; i < data.ActiveAbilities.Length; i++)
        {
            data.ActiveAbilities[i].stage = 0;
        }
        bufferedAction = null;
        animator.Play("Idle", 0);
        AbilitySlot.instances.Where(x => x.iconGO != null).ToList().ForEach(x => x.iconGO.GetComponent<ActiveAbilityIcon>().RefreshOnBar());
        StartActiveCooldown(active);
        GetComponent<FlashEffect>().FlashStart(Color.white, .2f);
    }

    protected void Follow(ActiveAbilityIcon active)
    {
        Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y);
        RaycastHit2D hit = Physics2D.Raycast(ray, ray, gameObject.layer);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<MinionEntity>() && hit.collider.gameObject != this)
            {
                data.currentForm = MinionData.Form.passive;
                //data.followTarget = hit.collider.gameObject.GetComponent<MinionEntity>().data;
                animator.SetBool("Following", true);
                //followController.target = data.followTarget.GetEntity().transform;
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
