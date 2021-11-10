using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public int coreSlots = 1;
    public PlayerCore[] cores;

    public int bodySlots = 10;
    public PlayerBody[] bodies;

    public GameObject golemBlueprint;
    public GameObject sentinelBlueprint;
    public GameObject automatonBlueprint;
    public GameObject UI;
    public InventoryDisplay inventoryDisplay;
    public Camera mainCam;
    public Camera uiCam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        cores = new PlayerCore[coreSlots];
        bodies = new PlayerBody[bodySlots];
    }

    public void Start()
    {
        GameObject userInterface = Instantiate(UI);
        userInterface.transform.GetChild(0).GetComponent<Canvas>().worldCamera = uiCam;
        inventoryDisplay = userInterface.transform.GetChild(0).GetChild(0).GetComponent<InventoryDisplay>();
        inventoryDisplay.uiCamera = uiCam;

        bodies[0] = new PlayerBody("Clay Golem");
        cores[0] = new PlayerCore(bodies[0], true);
        foreach (PlayerCore core in cores)
        {
            Spawn(core);
        }

    }

    public void Spawn(PlayerCore core)
    {
        VariantData variant = Database.GetVariant(core.currentBody.variantName);
        GameObject newPlayer;
        switch (variant.type)
        {
            case VariantData.Type.golem:
                newPlayer = Instantiate(golemBlueprint, transform);
                break;
            case VariantData.Type.sentinel:
                newPlayer = Instantiate(sentinelBlueprint, transform);
                break;
            case VariantData.Type.automaton:
                newPlayer = Instantiate(automatonBlueprint, transform);
                break;
            default:
                Debug.Log("failed to spawn");
                return;
        }
        newPlayer.GetComponent<PlayerEntity>().inventoryDisplay = inventoryDisplay;
        newPlayer.GetComponent<PlayerEntity>().activePlayer = core.activeCore;
        newPlayer.GetComponent<PlayerEntity>().movementSpeed = variant.moveSpeed;
        newPlayer.GetComponent<PlayerEntity>().jumpForce = variant.jumpForce;
        newPlayer.GetComponent<Animator>().runtimeAnimatorController = variant.animController;
        if (core.activeCore)
        {
            mainCam.GetComponent<CameraFollow>().followTransform = newPlayer.transform;
        }



    }


}
