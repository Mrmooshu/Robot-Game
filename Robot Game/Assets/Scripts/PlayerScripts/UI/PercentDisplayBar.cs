using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PercentDisplayBar : MonoBehaviour
{
    public delegate float floatDelegate();
    public floatDelegate currentValue;
    public floatDelegate barCapacity;
    protected Image bar;
    protected TextMeshProUGUI text;

    public virtual void Start()
    {
        bar = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Inititalize(floatDelegate current, floatDelegate capacity)
    {
        currentValue = current;
        barCapacity = capacity;
    }

    protected virtual void Update()
    {
        //bar.fillAmount = ((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).CurrentValue / ((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).Value;
        //text.text = (int)((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).CurrentValue + "/" + (int)((ResourceStat)PlayerManager.instance.activeMinion.GetEntity().stats[EntityStatType.Health]).Value;
        bar.fillAmount = currentValue.Invoke() / barCapacity.Invoke();
        text.text = (int)currentValue.Invoke() +  "/" + (int)barCapacity.Invoke();
    }
}
