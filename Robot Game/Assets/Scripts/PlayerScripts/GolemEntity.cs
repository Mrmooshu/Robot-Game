using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEntity : PlayerEntity
{
    public RockEntity currentRock;

    private bool mining;
    private float swingTimer;
    private float cooldown;

    [Header("Golem Specific Stats")]
    [SerializeField] public float miningPower;
    [SerializeField] public float miningSpeed;

    public override void Start()
    {
        base.Start();
        cooldown = 1.5f;
        swingTimer = cooldown;
        mining = false;
    }

    public override void Update()
    {
        base.Update();
        if (mining)
        {
            animator.SetBool("Mining", true);
            MiningLogic();
        }
        else
        {
            cantMove = false;
            animator.SetBool("Mining", false);
        }
    }

    public void MiningLogic()
    {
        if (swingTimer > 0)
        {
            swingTimer -= Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("Swing");
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Swing");
            swingTimer = cooldown;
        }
    }

    public void ToggleMining()
    {
        if (!mining)
        {
            if (weaponSlot != null && weaponSlot.weaponType == Weapon.Type.pickaxe)
            {
                transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = weaponSlot.animController;
                rigBod.velocity = Vector2.zero;
                mining = true;
                cantMove = true;
                return;
            }
            Debug.Log("need to equip a pick to mine");
        }
        else if (mining)
        {
            mining = false;
            cantMove = false;
        }

    }

    public void MineRock()
    {
        currentRock.RollDrop();
    }

}
