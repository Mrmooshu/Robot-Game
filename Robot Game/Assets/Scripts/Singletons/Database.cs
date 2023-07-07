using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour
{
    public static Database instance;

    public ItemDatabase itemDatabase;
    public EffectDatabase effectDatabase;
    public QuestDatabase questDatabase;
    public ActiveAbilityDatabase activeDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            itemDatabase.Initialize();
            effectDatabase.Initialize();
            activeDatabase.Initialize();

            //assign item id's
            for (int i = 0; i < itemDatabase.itemsList.Count; i++)
            {
                itemDatabase.itemsList[i].itemID = i + 1;
            }
            //assign effect id's
            for (int i = 0; i < effectDatabase.effectList.Count; i++)
            {
                effectDatabase.effectList[i].effectID = i + 1;
            }
        }
    }

    public static ItemData GetItem(int id)
    {
        return instance.itemDatabase.itemsList.FirstOrDefault(i => i.itemID == id);
    }
    public static int GetItemID(string name)
    {
        return instance.itemDatabase.itemsList.FirstOrDefault(i => i.itemName.ToLower() == name.ToLower()).itemID;
    }
    public static EffectData GetEffect(int id)
    {
        return instance.effectDatabase.effectList.FirstOrDefault(i => i.effectID == id);
    }
    public static int GetEffectID(string name)
    {
        return instance.effectDatabase.effectList.FirstOrDefault(i => i.effectName.ToLower() == name.ToLower()).effectID;
    }

    public static SafePointEntity GetSafePoint(string name)
    {
        return SafePointEntity.safepoints.FirstOrDefault(i => i.locationName == name);
    }
    public static Quest GetQuest(string name)
    {
        return instance.questDatabase.questList.FirstOrDefault(i => i.info.questName == name);
    }
    public static ActiveAbilityData GetActiveAbility(string name)
    {
        return instance.activeDatabase.ActivesList.FirstOrDefault(i => i.name == name);
    }
}