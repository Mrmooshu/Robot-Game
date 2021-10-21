using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnItem : MonoBehaviour
{
    public GameObject itemObject;
    public Item item;
    void Start()
    {
        GameObject newItem = Instantiate(itemObject, transform);
        newItem.GetComponent<ItemObject>().SetItem(item);
    }
}
