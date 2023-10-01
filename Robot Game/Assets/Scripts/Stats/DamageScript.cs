using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public static class DamageScript
{
    public enum damageType
    {
        physical, magic
    }

    public struct damageData
    {
        public float damage;
        public damageType type;

        public damageData(float damage, damageType type)
        {
            this.damage = damage;
            this.type = type;
        }
    }

    public static void ApplyDamage(Entity target, AttackData attackData)
    {
        //damage calculation
        int actualPhysicalDamage = 0;
        int actualMagicDamage = 0;
        List<damageData> damageSources = new List<damageData>();
        damageSources.Add(attackData.damageData);
        if (attackData.onHit && attackData.attacker is MinionEntity)
        {
            foreach (OnHitPassive onHitEffect in ((MinionEntity)attackData.attacker).onHitPassives)
            {
                damageSources.Add(onHitEffect.OnHit(target));
            }
        }
        foreach (damageData damage in damageSources)
        {
            if (damage.type is damageType.physical)
            {
                actualPhysicalDamage += Mathf.CeilToInt(damage.damage * (100 / (100 + target.stats[EntityStatType.AttackDefense].Value)));
            }
            else if (damage.type is damageType.magic)
            {
                actualMagicDamage += Mathf.CeilToInt(damage.damage * (100 / (100 + target.stats[EntityStatType.MagicDefense].Value)));
            }

        }
        ((ResourceStat)target.stats[EntityStatType.Health]).CurrentValue -= (actualPhysicalDamage + actualMagicDamage);
        int damageDisplayCount = 0;
        foreach ((int, Color) damage in new (int, Color)[] { (actualMagicDamage, Color.cyan), (actualPhysicalDamage, new Color(1, .74f, 0)) })
        {
            if (damage.Item1 > 0)
            {
                GameObject text = UnityEngine.Object.Instantiate(UIManager.instance.uiPrefabs.LoadAsset<GameObject>("Damage Text"));
                text.GetComponentInChildren<TextMeshProUGUI>().text = damage.Item1 + "";
                text.GetComponentInChildren<TextMeshProUGUI>().color = damage.Item2;
                text.transform.position = target.transform.position;
                text.transform.localScale = new Vector3(.02f, .02f, 1);
                text.transform.position += new Vector3(0, 1f + 1f * damageDisplayCount, 0);
                damageDisplayCount++;
            }
        }



    }
    /*
     * checks collision before damaging targets that are hit
     */
    public static bool Attack(AttackData attackData, Transform hitboxParent)
    {
        List<Collider2D> Totalhits = new List<Collider2D>();

        BoxCollider2D[] colliders = hitboxParent.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            if (collider.gameObject.activeInHierarchy)
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(collider.transform.position, collider.size, 0, attackData.targetMask);
                Totalhits.AddRange(hits);
            }
        }
        Totalhits.Distinct();

        foreach (Collider2D enemy in Totalhits)
        {
            ApplyDamage(enemy.transform.GetComponent<SlimeEnemy>(), attackData);
            KnockBack(enemy.transform.GetComponent<SlimeEnemy>(), attackData);
        }
        //return it the attack hit any targets
        return Totalhits.Count() > 0;
    }

    /*
     * damages directly
     */
    public static void Attack(AttackData attackData, Entity target)
    {
        ApplyDamage(target, attackData);
        KnockBack(target, attackData);
    }

    public static void KnockBack(Entity target, AttackData attackData)
    {
        target.GetComponent<Rigidbody2D>().velocity *= .1f;
        target.GetComponent<Rigidbody2D>().AddForce(target.animator.GetBool("Grounded") ? attackData.groundKnockback : attackData.airKockback);
        target.animator.SetFloat("Hitstun", attackData.hitStun);
        target.animator.SetTrigger("Hit");
        UniversalHelperFunctions.DelayedAction(() => { target.animator.ResetTrigger("Hit"); }, .1f);
    }
}
