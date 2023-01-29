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
        healthBar.fillAmount = ((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).CurrentValue / ((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).Value;
        text.text = (int)((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).CurrentValue +  "/" + (int)((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).Value;
    }
}
