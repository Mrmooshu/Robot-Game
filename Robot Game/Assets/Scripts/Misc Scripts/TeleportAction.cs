using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAction : MonoBehaviour
{
    public string safePointName = "default";

    public void Teleport()
    {
        PlayerManager.TeleportHere(safePointName);
        UIManager.CloseMainUi();
    }
}
