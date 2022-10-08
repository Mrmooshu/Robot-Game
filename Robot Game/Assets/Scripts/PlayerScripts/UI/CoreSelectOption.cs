using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreSelectOption : Toggle
{
    public PlayerCore core;

    protected override void Awake()
    {
        onValueChanged.AddListener(delegate { Selected(this); });
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
        CoreSelectDisplay.selectedCore = this;
        GameObject.Find("Second Camera").GetComponent<Camera>().GetComponent<TargetFollow>().followTransform = core.GetPlayer().transform;
        CoreInfoDisplay.UpdateInfo();
    }

}
