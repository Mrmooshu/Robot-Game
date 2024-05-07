using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassListOption : Toggle
{
    public UniversalPlayerData.minionunlock minion;

    protected override void Awake()
    {
        if (Application.isPlaying)
        {
            onValueChanged.AddListener(delegate { Selected(this); });
        }
    }

    public void Selected(Toggle t)
    {
        if (t.group.AnyTogglesOn())
        {
            RebirthMenu.instance.infoObject.SetActive(true);
            if (isOn)
            {
                RebirthMenu.instance.CurrentSelected = minion;
            }
        }
        else
        {
            RebirthMenu.instance.infoObject.SetActive(false);
        }
    }
}
