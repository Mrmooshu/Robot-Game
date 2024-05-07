using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RebirthMenu : SelectableMenu<UniversalPlayerData.minionunlock>
{
    public static RebirthMenu instance;

    public Button rebirthButton;

    protected override void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        base.Start();
        rebirthButton.onClick.AddListener(Rebirth);
    }

    public override void RefreshList()
    {
        base.RefreshList();

        foreach (UniversalPlayerData.minionunlock minion in PlayerManager.instance.universal.minionsUnlocked)
        {
            GameObject classOption = Instantiate(selectPrefab, transform);
            classOption.GetComponent<ClassListOption>().minion = minion;
            classOption.GetComponent<Toggle>().group = this;
        }
    }

    public override void RefreshInfo()
    {
        base.RefreshInfo();


    }

    private void Rebirth()
    {
        if (CurrentSelected.minionType != null)
        {
            if (PlayerManager.instance.universal.upgrades[$"{CurrentSelected.name} RebirthLevel"] < PlayerManager.instance.activeMinion.Level.Level)
            {
                PlayerManager.instance.universal.upgrades[$"{CurrentSelected.name} RebirthLevel"] = PlayerManager.instance.activeMinion.Level.Level;
            }

            PlayerManager.instance.ChangeMinionType(PlayerManager.instance.activeMinion, CurrentSelected.minionType);
            UIManager.CloseMainUi();
        }
    }
}
