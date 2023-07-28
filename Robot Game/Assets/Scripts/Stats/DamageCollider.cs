using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageScript;

public class DamageCollider : MonoBehaviour
{
    private LayerMask targetMask;
    private Entity attacker;
    private List<Entity> alreadyHit;
    private attackData attackData;

    public void Initialize(attackData attackData, LayerMask targetMask, Entity attacker)
    {
        this.targetMask = targetMask;
        alreadyHit = new List<Entity>();
        this.attacker = attacker;
        this.attackData = attackData;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UniversalHelperFunctions.LayerMaskCompare(targetMask, collision) && !alreadyHit.Contains(collision.GetComponent<Entity>()))
        {
            if (collision.GetComponent<Entity>() == null)
            {
                return;
            }
            DamageScript.Attack(attackData, collision.GetComponent<Entity>(), attacker);
            alreadyHit.Add(collision.GetComponent<Entity>());
        }
    }
}
