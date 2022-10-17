using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerEntity : Entity
{
    public PlayerCore core;
    public InteractableEntity currentInteractable;

    protected Rigidbody2D rigBod;
    protected Animator animator;
    protected Animator toolAnimator;
    protected Animator weaponAnimator;
    public Transform groundCheck;
    public float movementSpeed, jumpForce;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    protected float groundedRadius = .1f;

    private int movementInputDirection;
    public bool grounded, canJump;

    public void Initialize(PlayerCore core, float movementSpeed, float jumpForce, float gravity)
    {
        rigBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        toolAnimator = transform.GetChild(0).GetComponent<Animator>();
        weaponAnimator = transform.GetChild(1).GetComponent<Animator>();
        this.core = core;
        this.movementSpeed = movementSpeed;
        this.jumpForce = jumpForce;
        rigBod.gravityScale = gravity;
    }

    public override void Update()
    {
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
            if (!(animator.GetBool("Skilling") || animator.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack") || UIManager.instance.menuPreventingMovement) && PlayerManager.instance.activeCore == core)
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

            // attack
            if (Input.GetButtonDown("Attack1") && core.currentBody.weapon != null)
            {
                UpdateAnimators();
                animator.SetTrigger("Attack");
                weaponAnimator.SetTrigger("Attack");
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
        rigBod.velocity = new Vector2(movementSpeed * movementInputDirection, rigBod.velocity.y);

        // player running anim
        animator.SetInteger("Run", movementInputDirection);
    }

    public virtual void ToolAction()
    {

    }

    public virtual void BasicAttack()
    {
        if (core.currentBody.weapon != null)
        {
            Weapon weapon = (Weapon)Database.GetItem(core.currentBody.weapon.itemID);

            // Warhammer
            if (weapon is WarhammerWeapon)
            {
                WarhammerWeapon meleeWeapon = (WarhammerWeapon)weapon;

                List<Collider2D> TargetsHit = new List<Collider2D>();
                foreach (Weapon.HitColliderInfo collider in meleeWeapon.Hitcolliders)
                {
                    Collider2D[] hit = Physics2D.OverlapCircleAll(new Vector2((transform.position.x + collider.position.x * facingDirection), (transform.position.y + collider.position.y)), collider.radius, whatIsEnemy);
                    foreach (Collider2D target in hit)
                    {
                        if (!TargetsHit.Contains(target))
                        {
                            TargetsHit.Add(target);
                        }
                    }
                }
                // damage targets
            }
        }
    }

    protected void UpdateAnimators()
    {
        Tool tool = (Tool)Database.GetItem(core.currentBody.tool.itemID);
        Weapon weapon = (Weapon)Database.GetItem(core.currentBody.weapon.itemID);

        toolAnimator.runtimeAnimatorController = tool.animController;
        weaponAnimator.runtimeAnimatorController = weapon.animController;
    }
}
