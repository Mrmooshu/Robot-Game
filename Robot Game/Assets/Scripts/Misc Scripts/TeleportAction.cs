using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAction : MonoBehaviour
{
    public string safePointName = "default";

    public void Teleport()
    {
        if (PlayerManager.instance.universal.unlockedSafepoints.Contains(safePointName))
        {
            PlayerManager.TeleportHere(safePointName);
            UIManager.CloseMainUi();
        }
    }
}
