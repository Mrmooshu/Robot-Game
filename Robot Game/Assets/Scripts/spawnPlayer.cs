using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
