using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectDisplay : ToggleGroup
{
    public static PlayerSelectDisplay instance;

    public static PlayerSelectOption selectedPlayer;

    private GameObject playerPrefab;

    protected override void Start()
    {
        base.Start();
        if (instance == null)
        {
            instance = this;
        }

        playerPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("PlayerSelectOption");

        CreateList();

        selectedPlayer = GetComponentInChildren<PlayerSelectOption>();
        selectedPlayer.SelectInfoUpdate();
    }

    protected void CreateList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int counter = 0;
        foreach (PlayerData player in PlayerManager.instance.players)
        {
            GameObject playerOption = Instantiate(playerPrefab, transform);

            playerOption.GetComponent<PlayerSelectOption>().player = player;
            playerOption.GetComponent<Toggle>().group = this;
            playerOption.GetComponentInChildren<TextMeshProUGUI>().text = counter + "";

            counter++;
        }
    }

    public static void SwitchToSelectedCore()
    {
        PlayerManager.instance.SetActivePlayer(selectedPlayer.player);
        UIManager.CloseMainUi();
    }
}
