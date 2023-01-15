using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class DamageScript
{
    public static void ApplyDamage(Entity target, float damage)
    {
        ((ResourceStat)target.stats[StatType.Health]).currentValue -= damage;
        GameObject text = Object.Instantiate(UIManager.instance.uiPrefabs.LoadAsset<GameObject>("Damage Text"), target.transform);
        text.GetComponentInChildren<TextMeshProUGUI>().text = damage + "";
        text.transform.SetParent(text.transform.parent.parent);
        text.transform.localScale = new Vector3(.02f,.02f,1);
    }
}
