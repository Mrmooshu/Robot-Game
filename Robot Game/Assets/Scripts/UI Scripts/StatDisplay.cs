using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    public List<StatType> statsToDisplay;

    private TextMeshProUGUI textField;

    public void Start()
    {
        textField = GetComponentInChildren<TextMeshProUGUI>();
        RefreshStats();
        PlayerManager.instance.playerChanged += RefreshStats;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.playerChanged -= RefreshStats;
    }

    private void RefreshStats()
    {
        textField.text = "";
        foreach (StatType type in statsToDisplay)
        {
            if (PlayerManager.instance.activePlayer.GetEntity().stats.ContainsKey(type))
            {
                textField.text += type.ToString() + ":" + PlayerManager.instance.activePlayer.GetEntity().stats[type].Value + "\n";
            }
        }
    }
}
