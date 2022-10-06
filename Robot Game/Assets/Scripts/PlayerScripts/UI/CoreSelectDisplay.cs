using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoreSelectDisplay : ToggleGroup
{
    public static CoreSelectDisplay instance;

    public static CoreSelectOption selectedCore;

    private GameObject corePrefab;

    protected override void Start()
    {
        base.Start();
        if (instance == null)
        {
            instance = this;
        }

        corePrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("CoreSelectOption");

        CreateList();

        selectedCore = GetComponentInChildren<CoreSelectOption>();
        selectedCore.SelectInfoUpdate();
    }

    protected void CreateList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int counter = 0;
        foreach (PlayerCore core in PlayerManager.instance.cores)
        {
            GameObject coreOption = Instantiate(corePrefab, transform);

            coreOption.GetComponent<CoreSelectOption>().core = core;
            coreOption.GetComponent<Toggle>().group = this;
            coreOption.GetComponentInChildren<TextMeshProUGUI>().text = counter + "";

            counter++;
        }
    }

    public static void SwitchToSelectedCore()
    {
        PlayerManager.instance.SetActiveCore(selectedCore.core);
        UIManager.CloseMainUi();
    }
}
