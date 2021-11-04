using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnPlayer : MonoBehaviour
{
    public InventoryDisplay inventoryDisplay;
    public GameObject playerObject;
    public Camera cam;
    public bool activePlayer = false;
    void Start()
    {
        GameObject newPlayer = Instantiate(playerObject, transform);
        newPlayer.GetComponent<PlayerEntity>().inventoryDisplay = inventoryDisplay;
        newPlayer.GetComponent<PlayerEntity>().activePlayer = activePlayer;
        cam.GetComponent<CameraFollow>().followTransform = newPlayer.transform;

        PlayerEntity newPlayerEntity = newPlayer.GetComponent<PlayerEntity>();

        //temp access to save and load buttons
        inventoryDisplay.transform.parent.GetChild(3).GetComponent<Button>().onClick.AddListener(newPlayerEntity.SavePlayerData);
        inventoryDisplay.transform.parent.GetChild(4).GetComponent<Button>().onClick.AddListener(newPlayerEntity.LoadPlayerData);
    }
}
