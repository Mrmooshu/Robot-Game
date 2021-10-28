using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour
{
    private static Database instance;

    public ItemDatabase itemDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < itemDatabase.itemsList.Count; i++)
            {
                itemDatabase.itemsList[i].itemID = i;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static ItemData GetItem(int id)
    {
        return instance.itemDatabase.itemsList.FirstOrDefault(i => i.itemID == id);
    }
}