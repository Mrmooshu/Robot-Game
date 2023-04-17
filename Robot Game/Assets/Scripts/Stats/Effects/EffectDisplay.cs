using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplay : MonoBehaviour
{
    protected GameObject effectPrefab;
    public Transform buffArea;
    public Transform debuffArea;

    public void Start()
    {
        effectPrefab = UIManager.instance.uiPrefabs.LoadAsset<GameObject>("EffectObject");
        RefreshEffects();
        PlayerManager.instance.minionChanged += RefreshEffects;
        MinionEntity.effectUpdated += RefreshEffects;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.minionChanged -= RefreshEffects;
        MinionEntity.effectUpdated -= RefreshEffects;
    }

    public virtual void RefreshEffects()
    {
        CreateEffects();
    }

    protected virtual void CreateEffects()
    {
        foreach (Transform child in buffArea)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in debuffArea)
        {
            Destroy(child.gameObject);
        }
        Dictionary<int, Effect> effects = PlayerManager.instance.activeMinion.GetEntity().effects;

        int buffCount = 0;
        int debuffCount = 0;
        float space = 10;
        foreach (KeyValuePair<int, Effect> effect in effects)
        {
            GameObject effectInstance = Instantiate(effectPrefab, buffArea.parent);
            if (effect.Value.effectData is BuffData)
            {
                effectInstance.transform.SetParent(buffArea.transform);
                effectInstance.transform.localPosition = new Vector2(buffCount * space, 0);
            }
            else if (effect.Value.effectData is DebuffData)
            {
                effectInstance.transform.SetParent(debuffArea.transform);
                effectInstance.transform.localPosition = new Vector2(debuffCount* space, 0);
            }
            effectInstance.transform.GetComponent<Image>().sprite = effect.Value.effectData.sprite;
            effectInstance.GetComponent<EffectObject>().Intitalize(effect.Value);
        }

    }
}
