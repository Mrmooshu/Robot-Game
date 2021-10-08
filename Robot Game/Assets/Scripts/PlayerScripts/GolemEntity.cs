using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEntity : PlayerEntity
{
    public bool mining;
    [SerializeField] public float miningPower;
    [SerializeField] public float miningSpeed;

    private float swingTimer;
    private float cooldown;

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
        if (rigBod.velocity.magnitude > 0)
        {
            mining = false;
        }
        if (mining)
        {
            animator.SetBool("Mining", true);
            MiningLogic();
        }
        else
        {
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
            swingTimer = cooldown;
            Debug.Log("lets goooo");
        }
    }

    public void StartMining()
    {
        rigBod.velocity = Vector2.zero;
        mining = true;
    }

    public void GetDrop()
    {
        Debug.Log("drop something now");
    }

}
