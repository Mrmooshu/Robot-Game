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

    public static void CloseMainUi()
    {
        RemoteMenuToggle.ToggleThis("CloseToggle");
    }
}
