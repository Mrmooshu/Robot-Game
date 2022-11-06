using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour
{
    public static Database instance;

    public ItemDatabase itemDatabase;
    public EffectDatabase effectDatabase;
    public VariantDatabase variantDatabase;
    public SafePointDatabase safePointDatabase;
    public QuestDatabase questDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            itemDatabase.Initialize();

            //assign item id's
            for (int i = 0; i < itemDatabase.itemsList.Count; i++)
            {
                itemDatabase.itemsList[i].itemID = i + 1;
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
    public static BuffData GetBuff(string name)
    {
        return instance.effectDatabase.buffList.FirstOrDefault(i => i.buffName.ToLower() == name.ToLower());
    }

    public static DebuffData GetDebuff(string name)
    {
        return instance.effectDatabase.debuffList.FirstOrDefault(i => i.debuffName.ToLower() == name.ToLower());
    }

    public static VariantData GetVariant(string name)
    {
        return instance.variantDatabase.variantList.FirstOrDefault(i => i.variantName == name);
    }

    public static SafePointData GetSafePoint(string name)
    {
        return instance.safePointDatabase.safePointList.FirstOrDefault(i => i.locationName == name);
    }

    public static Quest GetQuest(string name)
    {
        return instance.questDatabase.questList.FirstOrDefault(i => i.info.questName == name);
    }
}