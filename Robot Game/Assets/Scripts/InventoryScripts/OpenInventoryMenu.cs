using System.IO;
using UnityEngine;

public class OpenInventoryMenu : MonoBehaviour
{
    public void ToggleMenu()
    {

        if (UIManager.instance.currentMenu != null)
        {
            if (UIManager.instance.currentMenu.name == "InventoryMenu(Clone)")
            {
                UIManager.CloseMenu();
                return;
            }
        }
        GameObject menu = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("InventoryMenu");
        UIManager.ChangeMenu(menu);
    } 
}
