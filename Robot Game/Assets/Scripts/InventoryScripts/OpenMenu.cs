using System.IO;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public string menuName = "";
    public void ToggleMenu()
    {
        if (UIManager.instance.currentMenu != null)
        {
            UIManager.CloseMenu();
            return;
        }
        if (menuName != "")
        {
            GameObject menu = UIManager.instance.uiPrefabs.LoadAsset<GameObject>(menuName);
            UIManager.ChangeMenu(menu);
        }
    } 
}
