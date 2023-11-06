using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterTitleDisplay : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI level;

    void Start()
    {
        Refresh();
        PlayerManager.instance.minionChanged += Refresh;
        LevelData.levelup += Refresh;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= Refresh;
        LevelData.levelup -= Refresh;
    }

    private void Refresh()
    {
        title.text = PlayerManager.instance.activeMinion.variantName;
        level.text = "Lvl:" + PlayerManager.instance.activeMinion.Level.level.ToString();
    }
}
