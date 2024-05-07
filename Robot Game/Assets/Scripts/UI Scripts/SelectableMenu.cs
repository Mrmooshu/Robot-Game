using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public abstract class SelectableMenu<T> : ToggleGroup
{
    private T currentSelected;
    public T CurrentSelected { get { return currentSelected; } set { currentSelected = value;  RefreshInfo(); } }

    public GameObject selectPrefab;

    public GameObject infoObject;

    public Toggle[] toggles;

    public bool Filtered { get { return toggles.Any(x => x.isOn == true); } }

    protected override void Start()
    {
        foreach(Toggle a in toggles)
        {
            a.onValueChanged.AddListener(delegate { RefreshList(); });
        }
        RefreshList();
    }

    protected override void OnDestroy()
    {
        foreach (Toggle t in toggles)
        {
            t.onValueChanged.RemoveAllListeners();
        }
    }

    protected override void OnDisable()
    {
        foreach (Toggle t in toggles)
        {
            t.isOn = false;
        }
    }

    public virtual void RefreshList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public virtual void RefreshInfo()
    {

    }
}
