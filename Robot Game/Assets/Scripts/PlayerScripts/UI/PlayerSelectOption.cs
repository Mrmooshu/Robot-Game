using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerSelectOption : Toggle
{
    public PlayerData player;

    protected override void Awake()
    {
        onValueChanged.AddListener(delegate { Selected(this); });
        PlayerManager.instance.playerChanged += UpdateCamera;
        UpdateCamera();
    }

    public void Selected(Toggle t)
    {
        if (t.group.AnyTogglesOn())
        {
            if (isOn)
            {
                SelectInfoUpdate();
            }
        }
    }

    public void SelectInfoUpdate()
    {
        PlayerSelectDisplay.selectedPlayer = this;
        GameObject.Find("Second Camera").GetComponent<Camera>().GetComponent<TargetFollow>().followTransform = player.GetEntity().transform;
        CoreInfoDisplay.UpdateInfo();
    }

    private void UpdateCamera()
    {
        if (isOn)
        {
            GameObject.Find("Second Camera").GetComponent<Camera>().GetComponent<TargetFollow>().followTransform = player.GetEntity().transform;
        }
    }

}
