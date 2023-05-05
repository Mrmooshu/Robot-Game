using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    protected Image healthBar;
    protected TextMeshProUGUI text;

    public virtual void Start()
    {
        healthBar = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        healthBar.fillAmount = ((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).CurrentValue / ((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).Value;
        text.text = (int)((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).CurrentValue +  "/" + (int)((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).Value;
    }
}
