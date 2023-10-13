using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject currentMenu;
    public Transform canvasUI;
    public AssetBundle uiPrefabs;
    public SpriteAtlas uiSprites;

    public GameObject mainInterface;
    public ActionButton actionButton;

    public bool menuPreventingMovement = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            uiPrefabs = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "ui prefabs"));
            GameObject mainUI = uiPrefabs.LoadAsset<GameObject>("UI");

            canvasUI = GameObject.Find("UI Canvas").transform;
            mainInterface = Instantiate(mainUI, canvasUI);

            actionButton = mainInterface.GetComponentInChildren<ActionButton>();
        }
    }

    public static void ChangeMenu(GameObject menu, bool preventMovement = false)
    {
        if (instance.currentMenu != null)
        {
            Destroy(instance.currentMenu);
        }
        instance.currentMenu = Instantiate(menu, instance.canvasUI);

        if (preventMovement)
        {
            instance.menuPreventingMovement = true;
        }
    }

    public static void CloseMenu()
    {
        if (instance.currentMenu != null)
        {
            Destroy(instance.currentMenu);
        }
        if (instance.menuPreventingMovement == true)
        {
            instance.menuPreventingMovement = false;
        }
    }

    public static void CloseMainUi()
    {
        GameObject.Find("UI Canvas/UI(Clone)/SafePointMenu").SetActive(false);
        GameObject.Find("UI Canvas/UI(Clone)/MainPlayerMenu").SetActive(false);
        GameObject.Find("UI Canvas/UI(Clone)/SwapPlayerMenu").SetActive(false);
        GameObject.Find("UI Canvas/UI(Clone)/Skill Interfaces/Smithing Menu").SetActive(false);
        GameObject.Find("UI Canvas/UI(Clone)").GetComponent<ToggleGroup>().SetAllTogglesOff();
    }
}
