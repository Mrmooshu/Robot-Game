using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : NPCBehaviour
{
    private bool returningToRange = false;

    public WanderBehaviour(CharacterEntity entity) : base(entity)
    {
    }

    public void Wander(CharacterEntity entity)
    {
        if (entity.walkTimer > 0)
        {
            entity.walkTimer--;
        }
        if (entity.waitTimer > 0)
        {
            entity.waitTimer--;
        }
        if (entity.waitTimer == 0 && entity.walkTimer == 0)
        {
            returningToRange = false;
            DecideMovement();
        }
        if (!returningToRange && entity.maxDistanceFromSpawn != 0 && (entity.transform.position.x > entity.spawnX + entity.maxDistanceFromSpawn || entity.transform.position.x < entity.spawnX - entity.maxDistanceFromSpawn))
        {
            StartReturningToRange();
        }
        CheckForJump();
    }

    void StartWalking(int duration)
    {
        entity.walkTimer = duration;
    }

    protected virtual void StartWaiting(int duration)
    {
        entity.waitTimer = duration;
    }

    protected virtual void DecideMovement()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                StartWalking(Random.Range(40, 180));
                break;
            case 1:
                StartWaiting(Random.Range(20, 120));
                break;
            case 2:
                entity.Flip();
                StartWaiting(Random.Range(10, 80));
                break;
        }
    }

    protected void CheckForJump()
    {
        if (Physics2D.Raycast(entity.wallCheck.position, new Vector2(entity.facingDirection,0), .7f, entity.whatIsGround) &&
            !Physics2D.Raycast(entity.jumpCheck.position, new Vector2(entity.facingDirection, 0), .7f, entity.whatIsGround))
        {
            entity.waitTimer = 0;
            entity.Jump();
            StartWalking(Random.Range(60, 120));
        }
    }

    protected void StartReturningToRange()
    {
        if (entity.transform.position.x > entity.spawnX)
        {
            entity.SetDirection(-1);
        }
        else if (entity.transform.position.x < entity.spawnX)
        {
            entity.SetDirection(1);
        }
        StartWalking(60);
        returningToRange = true;
    }
}
