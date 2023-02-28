using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public static class DamageScript
{
    public enum damageType
    {
        physical,magic
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

    public struct attackData
    {
        public Entity attacker;
        public damageData damageData;
        public bool onHit;
        public Vector2 knockback;
        public float hitStun;

        public attackData(Entity attacker, damageData damageData, bool onHit, Vector2 knockback, float hitStun)
        {
            this.attacker = attacker;
            this.damageData = damageData;
            this.onHit = onHit;
            this.knockback = knockback;
            this.hitStun = hitStun;
        }
    }

    public static void ApplyDamage(Entity target, attackData attackData)
    {
        //damage calculation
        int actualPhysicalDamage = 0;
        int actualMagicDamage = 0;
        List<damageData> damageSources = new List<damageData>();
        damageSources.Add(attackData.damageData);
        if (attackData.onHit && attackData.attacker is PlayerEntity)
        {
            foreach (IOnHit onHitEffect in ((PlayerEntity)attackData.attacker).onHitPassives)
            {
                damageSources.Add(onHitEffect.OnHit(target));
            }
        }
        foreach (damageData damage in damageSources)
        {
            if (damage.type is damageType.physical)
            {
                actualPhysicalDamage = Mathf.CeilToInt(damage.damage * (100 / (100 + target.stats[StatType.AttackDefense].Value)));
            }
            else if (damage.type is damageType.magic)
            {
                actualMagicDamage = Mathf.CeilToInt(damage.damage * (100 / (100 + target.stats[StatType.MagicDefense].Value)));
            }

        }
        ((ResourceStat)target.stats[StatType.Health]).CurrentValue -= (actualPhysicalDamage + actualMagicDamage);
        int damageDisplayCount = 0;
        foreach ((int,Color) damage in new (int, Color)[]{(actualMagicDamage,Color.cyan), (actualPhysicalDamage, new Color(1,.74f,0)) })
        {
            if (damage.Item1 > 0)
            {
                GameObject text = Object.Instantiate(UIManager.instance.uiPrefabs.LoadAsset<GameObject>("Damage Text"), target.transform);
                text.GetComponentInChildren<TextMeshProUGUI>().text = damage.Item1 + "";
                text.GetComponentInChildren<TextMeshProUGUI>().color = damage.Item2;
                text.transform.SetParent(text.transform.parent.parent);
                text.transform.localScale = new Vector3(.02f, .02f, 1);
                text.transform.position += new Vector3(0,.5f*damageDisplayCount,0);
                damageDisplayCount++;
            }
        }



    }

    public static void Attack(attackData attackData, Transform hitboxParent, LayerMask targetMask, Entity attacker)
    {
        List<Collider2D> Totalhits = new List<Collider2D>();

        BoxCollider2D[] colliders = hitboxParent.GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            if (collider.gameObject.activeInHierarchy)
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(collider.transform.position, collider.size, 0, targetMask);
                Totalhits.AddRange(hits);
            }
        }
        Totalhits.Distinct();

        foreach (Collider2D enemy in Totalhits)
        {
            ApplyDamage(enemy.transform.GetComponent<EnemyEntity>(), attackData);
            KnockBack(enemy.transform.GetComponent<EnemyEntity>(), attackData);
        }
    }

    public static void KnockBack(Entity target, attackData attackData)
    {
        target.GetComponent<Rigidbody2D>().AddForce(attackData.knockback);
        target.StartCoroutine(HitStunRoutine(target, attackData.hitStun));
    }

    private static IEnumerator HitStunRoutine(Entity target, float duration)
    {
        target.stunned = true;
        yield return new WaitForSeconds(duration);
        target.stunned = false;
    }
}
