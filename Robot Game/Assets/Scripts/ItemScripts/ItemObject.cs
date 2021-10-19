using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
}
