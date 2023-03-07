using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor.Animations;

public class PlayerEntity : UnitEntity
{
    public PlayerData data;

    public List<IOnHit> onHitPassives;

    public Interactable currentInteractable;

    AnimatorController controller;

    //gameobject components

    //other
    public Transform hitboxes;

    public virtual void Initialize(PlayerData data)
    {
        this.data = data;
        onHitPassives = new List<IOnHit>();
        base.Initialize();
        controller = (AnimatorController)animator.runtimeAnimatorController;
    }

    protected override void Input()
    {
        base.Input();

        if (grounded && rigBod.velocity.y <= 0)
        {
            canJump = true;
            animator.SetBool("Jumping", false);
        }

        if (PlayerManager.instance.activePlayer == data)
        {
            // movement
            if (!(animator.GetCurrentAnimatorStateInfo(0).IsTag("stuckaction") || UIManager.instance.menuPreventingMovement) && PlayerManager.instance.activePlayer == data)
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
                if (data.weapon != null)
                {
                    var state = controller.layers[2].stateMachine.states.FirstOrDefault(s => s.state.name.Equals("Basic")).state;
                    controller.SetStateEffectiveMotion(state, ((Weapon)Database.GetItem(data.weapon.itemID)).animation);
                    animator.Play(Database.GetItem(data.weapon.itemID).GetType().ToString() + " Basic", 0);
                    animator.Play("Basic", 2);
                }

            }

            // left click is down
            if (UnityEngine.Input.GetMouseButton(0) && PlayerManager.instance.activePlayer == data)
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

    public virtual void BasicAttack()
    {
        animator.ResetTrigger("Basic");
        ((Weapon)Database.GetItem(data.weapon.itemID)).BasicAttack(this);


        //Vector2 knockback = new Vector2(100 * facingDirection, 100);
        //DamageScript.Attack(new DamageScript.attackData(this, new DamageScript.damageData(stats[StatType.AttackDamage].Value, DamageScript.damageType.physical), true, knockback, .5f), hitboxes, whatIsEnemy, this);
        //rigBod.AddForce(new Vector2(stats[StatType.MoveSpeed].Value * facingDirection, 1.2f), ForceMode2D.Impulse);
        //kill switch (uncomment to kill urself)
        //((ResourceStat)stats[StatType.Health]).CurrentValue -= 1000;
    }

    public void OnDrawGizmos()
    {
        if (hitboxes.GetComponentsInChildren<BoxCollider2D>().Length > 0)
        {
            Gizmos.DrawWireCube(hitboxes.GetComponentsInChildren<BoxCollider2D>()[0].transform.position, hitboxes.GetComponentsInChildren<BoxCollider2D>()[0].size);
        }
    }
}
