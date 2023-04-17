using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    public List<StatType> statsToDisplay;

    private TextMeshProUGUI textField;

    private Dictionary<StatType, Stat> stats;

    public void Start()
    {
        textField = GetComponentInChildren<TextMeshProUGUI>();
        RefreshStats();
        PlayerManager.instance.minionChanged += RefreshStats;
        SubscribeToStats();
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= RefreshStats;
        if (stats != null)
        {
            foreach (var stat in stats)
            {
                stat.Value.statUpdated -= RefreshStats;
            }
        }
    }

    private void RefreshStats()
    {
        textField.text = "";
        foreach (StatType type in statsToDisplay)
        {
            if (PlayerManager.instance.activeMinion.GetEntity().stats.ContainsKey(type))
            {
                textField.text += type.ToString() + ":" + PlayerManager.instance.activeMinion.GetEntity().stats[type].Value + "\n";
            }
        }
    }

    private void SubscribeToStats()
    {
        //unsub from previous stats
        if (stats != null)
        {
            foreach (var stat in stats)
            {
                stat.Value.statUpdated -= RefreshStats;
            }
        }
        //change stats to new stats
        stats = PlayerManager.instance.activeMinion.GetEntity().stats;
        //sub to new stats
        foreach (var stat in stats)
        {
            stat.Value.statUpdated += RefreshStats;
        }
    }
}
