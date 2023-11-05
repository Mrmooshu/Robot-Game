using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageScript;
using System.Linq;

public class HitboxScript : MonoBehaviour
{
    public AttackData attack = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Hit(Collider2D collider)
    {
        if (attack != null)
        {
            if (!attack.Totalhits.Contains(collider.transform.GetComponent<Entity>()) && UniversalHelperFunctions.LayerMaskCompare(attack.targetMask, collider))
            {
                attack.hit = true;
                attack.Totalhits.Add(collider.transform.GetComponent<Entity>());
                ApplyDamage(collider.transform.GetComponent<Entity>(), attack);
                KnockBack(collider.transform.GetComponent<Entity>(), attack);
                attack.ExecuteActions(AttackData.effectOccurance.hit);
            }
        }
    }

    public void CheckToEnd()
    {
        if (GetComponentsInChildren<HitboxChildScript>().All(x => x.gameObject.activeSelf == false) && attack != null)
        {
            attack.ExecuteActions(AttackData.effectOccurance.end);
            attack = null;
        }
    }

    public void BeginAttack()
    {
        attack.ExecuteActions(AttackData.effectOccurance.start);
    }

    public AttackData EnableAttack(AttackData attack)
    {
        this.attack = attack;

        return attack;
    }
}
