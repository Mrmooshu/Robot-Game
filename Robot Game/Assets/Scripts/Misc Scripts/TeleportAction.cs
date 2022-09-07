using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAction : MonoBehaviour
{
    public string safePointName = "SafePoint 1";

    public void Teleport()
    {
        PlayerManager.TeleportHere(safePointName);
        UIManager.CloseMenu();
    }
}
