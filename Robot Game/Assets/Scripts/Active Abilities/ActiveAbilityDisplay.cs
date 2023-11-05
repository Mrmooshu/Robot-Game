using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityDisplay : ToggleGroup
{
    public GameObject ActiveAbilityPrefab;
    public TextMeshProUGUI activeNameText;
    public TextMeshProUGUI activeDescriptionText;
    public Toggle GeneralToggle;
    public Toggle CurrentToggle;

    public static ActiveAbilityDisplay instance;

    public ActiveAbilityIcon selected;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }

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
        RefreshInfoWindow();
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
        foreach (var active in PlayerManager.instance.universal.abilities)
        {
            if (active.Value != false)
            {
                GameObject origin = new GameObject();
                origin.transform.SetParent(transform);
                origin.transform.localPosition = new Vector2((x * slotSize), (-y * slotSize));
                GameObject activeAbilityIconInstance = Instantiate(ActiveAbilityPrefab, transform);
                activeAbilityIconInstance.GetComponent<ActiveAbilityIcon>().activeAbility = active.Key;
                activeAbilityIconInstance.transform.SetParent(origin.transform);
                activeAbilityIconInstance.transform.localPosition = Vector3.zero;
                x++;
                if (x >= columns)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }

    protected override void OnEnable()
    {
        RefreshInfoWindow();
    }

    public void RefreshInfoWindow()
    {
        if (selected != null)
        {
            activeNameText.enabled = true;
            activeDescriptionText.enabled = true;
            activeNameText.text = Database.GetActiveAbility(selected.activeAbility).name;
            activeDescriptionText.text = Database.GetActiveAbility(selected.activeAbility).Description;
        }
        else
        {
            activeNameText.enabled = false;
            activeDescriptionText.enabled = false;
        }
    }
}
