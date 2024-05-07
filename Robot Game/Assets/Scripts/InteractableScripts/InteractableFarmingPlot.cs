using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableFarmingPlot : Interactable
{
    public static List<InteractableFarmingPlot> plots = new List<InteractableFarmingPlot>();

    public GameObject slot;
    public string plotname = "defaultplotname";
    public FarmingData.PlotData.PlotType type = FarmingData.PlotData.PlotType.tree;

    private void Start()
    {
        plots.Add(this);
        if (!PlayerManager.instance.farming.plots.ContainsKey(plotname))
        {
            PlayerManager.instance.farming.plots.Add(plotname, new FarmingData.PlotData(type));
        }

    }

    public override void PlayerInRange(MinionEntity playerEntitiy)
    {
        if (playerEntitiy.data == PlayerManager.instance.activeMinion)
        {
            UIManager.instance.actionButton.SetCurrentButton(Action, icon);
            playerEntitiy.currentInteractable = this;
        }
    }

    private void Action()
    {
        RemoteMenuToggle.ToggleThis("FarmingToggle");
        PlayerManager.instance.farming.currentPlot = PlayerManager.instance.farming.plots[plotname];
    }

    private void Update()
    {
        if (PlayerManager.instance.farming.plots[plotname].currentseed != null && slot.transform.childCount == 0)
        {
            Instantiate(PlayerManager.instance.farming.plots[plotname].currentseed.treefab, slot.transform);
            slot.transform.GetChild(0).Find($"Stage {PlayerManager.instance.farming.plots[plotname].currentstage}").GetComponent<Toggle>().isOn = true;
        }

        if (PlayerManager.instance.farming.plots[plotname].stagedurationcountdown > 0)
        {
            PlayerManager.instance.farming.plots[plotname].stagedurationcountdown -= Time.deltaTime;
            if (PlayerManager.instance.farming.plots[plotname].stagedurationcountdown <= 0)
            {
                PlayerManager.instance.farming.plots[plotname].currentstage--;
                slot.transform.GetChild(0).Find($"Stage {PlayerManager.instance.farming.plots[plotname].currentstage}").GetComponent<Toggle>().isOn = true;
                if (PlayerManager.instance.farming.plots[plotname].currentstage >= 1)
                {
                    PlayerManager.instance.farming.plots[plotname].stagedurationcountdown = PlayerManager.instance.farming.plots[plotname].currentseed.stageduration;
                }
                else
                {
                    PlayerManager.instance.farming.plots[plotname].stagedurationcountdown = 0;
                }
            }
        }
    }
}
