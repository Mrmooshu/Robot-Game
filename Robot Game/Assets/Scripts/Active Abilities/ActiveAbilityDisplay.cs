using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityDisplay : ToggleGroup
{
    public GameObject ActiveAbilityPrefab;
    public Toggle GeneralToggle;
    public Toggle CurrentToggle;

    protected override void Start()
    {
        base.Start();
        GeneralToggle.onValueChanged.AddListener(delegate { RefreshList(); });
        CurrentToggle.onValueChanged.AddListener(delegate { RefreshList(); });
        RefreshList();
    }

    protected override void OnDestroy()
    {
        GeneralToggle.onValueChanged.RemoveAllListeners();
        CurrentToggle.onValueChanged.RemoveAllListeners();
    }

    public virtual void RefreshList()
    {
        CreateList();
    }

    protected void CreateList()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        int columns = 5;
        float slotSize = 39f;
        foreach (var active in UniversalPlayerData.abilities)
        {
            if (active.Value != false)
            {
                GameObject activeAbilityIconInstance = Instantiate(ActiveAbilityPrefab, transform);
                activeAbilityIconInstance.GetComponent<ActiveAbilityIcon>().activeAbility = active.Key;
                activeAbilityIconInstance.transform.localPosition = new Vector2((x * slotSize), (-y * slotSize));
                x++;
                if (x >= columns)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }
}
