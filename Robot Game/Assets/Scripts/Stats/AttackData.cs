using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageScript;

public class AttackData
{
    public enum effectOccurance
    {
        start, end, hit
    }

    public Entity attacker;
    public damageData damageData;
    public bool onHit;
    public Vector2 groundKnockback;
    public Vector2 airKockback;
    public float hitStun;
    public LayerMask targetMask;
    private List<(Action, effectOccurance)> additionalActions = new List<(Action, effectOccurance)>();

    public bool hit = false;
    public List<Entity> Totalhits = new List<Entity>();

    public AttackData(Entity attacker, damageData damageData, bool onHit, (Vector2, Vector2) knockback, float hitStun, LayerMask targetMask)
    {
        this.attacker = attacker;
        this.damageData = damageData;
        this.onHit = onHit;
        this.groundKnockback = knockback.Item1;
        this.airKockback = knockback.Item2;
        this.hitStun = hitStun;
        this.targetMask = targetMask;
    }

    public void AddAction((Action, effectOccurance) action)
    {
        additionalActions.Add(action);
    }

    public void ExecuteActions(effectOccurance occurance)
    {
        foreach (var action in additionalActions)
        {
            if (action.Item2 == occurance)
            {
                action.Item1();
            }
        }
    }
}
