using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject currentMenu;
    public Transform canvasUI;
    public AssetBundle uiPrefabs;
    public SpriteAtlas uiSprites;

    public GameObject mainInterface;
    public ActionButton actionButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Initialize()
    {
        uiPrefabs = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "ui prefabs"));
        GameObject mainUI = uiPrefabs.LoadAsset<GameObject>("UI");

        canvasUI = GameObject.Find("UI Canvas").transform;
        mainInterface = Instantiate(mainUI, canvasUI);

        actionButton = mainInterface.GetComponentInChildren<ActionButton>();
    }

    public static void ChangeMenu(GameObject menu)
    {
        if (instance.currentMenu != null)
        {
            Destroy(instance.currentMenu);
        }
        instance.currentMenu = Instantiate(menu, instance.canvasUI);

    }

    public static void CloseMenu()
    {
        if (instance.currentMenu != null)
        {
            Destroy(instance.currentMenu);
        }
    }
}
