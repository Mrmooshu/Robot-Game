using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int coreSlots = 1;
    public PlayerCore[] cores;

    public int bodySlots = 10;
    public PlayerBody[] bodies;

    public GameObject golemBlueprint;
    public GameObject sentinelBlueprint;
    public GameObject automatonBlueprint;
    public Camera mainCam;
    public Camera uiCam;


    public PlayerCore activeCore;

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
    }

    public void Initialize()
    {
        cores = new PlayerCore[coreSlots];
        bodies = new PlayerBody[bodySlots];

        bodies[0] = new PlayerBody("Clay Golem");
        bodies[1] = new PlayerBody("Snow Golem");
        cores[0] = new PlayerCore(bodies[0], 36, 10);
        cores[1] = new PlayerCore(bodies[1], 10, 10);
        foreach (PlayerCore core in cores)
        {
            if (core != null)
            {
                Spawn(core);
            }
        }
        SetActiveCore(cores[0]);
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
        PlayerEntity playerEntity = newPlayer.GetComponent<PlayerEntity>();
        playerEntity.Initialize(core, variant.moveSpeed, variant.jumpForce);
        newPlayer.GetComponent<Animator>().runtimeAnimatorController = variant.animController;
        core.bodyObject = newPlayer;
    }

    public void SetActiveCore(PlayerCore core)
    {
        activeCore = core;
        ControlThisPlayer(core.bodyObject.GetComponent<PlayerEntity>());
    }

    public void ControlThisPlayer(PlayerEntity player)
    {
        player.TakeControl();
        if (player.currentInteractable != null)
        {
            player.currentInteractable.PlayerInRange(player);
        }
        else
        {
            UIManager.instance.actionButton.SetCurrentButton(ActionButton.buttons.none);
        }

    }

}
