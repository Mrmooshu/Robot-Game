using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FunctionDisplay : MonoBehaviour
{
    public int functionslotindex;
    public GameObject bar;
    public TextMeshProUGUI title;
    public TextMeshProUGUI level;
    public FunctionInventorySlot slot;

    void Start()
    {
        Refresh();
        PlayerManager.instance.minionChanged += Refresh;
        LevelData.levelup += Refresh;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= Refresh;
        LevelData.levelup -= Refresh;
    }

    public void Refresh()
    {
        Create();
    }

    public void Create()
    {
        if (PlayerManager.instance.activeMinion.functions[functionslotindex] == null)
        {
            SetOn(false);
            return;
        }
        SetOn(true);
        var function = PlayerManager.instance.activeMinion.functions[functionslotindex];
        title.text = function.name;
        level.text = "Lvl:" + function.level.Level.ToString();
        if (!bar.GetComponent<PercentDisplayBar>())
        {
            bar.AddComponent<PercentDisplayBar>();
        }
        var percentbar = bar.GetComponent<PercentDisplayBar>();
        percentbar.Inititalize(() => { return function.level.Exp - LevelData.GetExpForNextLevel(function.level.Level-1); },() => { return LevelData.GetExpForNextLevel(function.level.Level) - LevelData.GetExpForNextLevel(function.level.Level-1); });
        slot.functionSlotIndex = functionslotindex;



        void SetOn(bool state)
        {
            bar.SetActive(state);
            title.gameObject.SetActive(state);
            level.gameObject.SetActive(state);
            slot.gameObject.SetActive(state);
        }
    }
}
