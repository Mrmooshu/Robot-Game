using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    private TextMeshProUGUI text;

    void Start()
    {
        healthBar = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        healthBar.fillAmount = ((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).currentValue / ((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).Value;
        text.text = ((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).currentValue +  "/" + ((ResourceStat)PlayerManager.instance.activeCore.GetPlayer().stats[StatType.Health]).Value;
    }
}
