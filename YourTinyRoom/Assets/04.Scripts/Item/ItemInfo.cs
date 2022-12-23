using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    public Item.ItemType itemType;
    public int quantity=1;
    private void Awake()
    {
        itemType = item.itemType;
        if (GetComponent<SpriteRenderer>() == null)
        { 
            GetComponentInChildren<SpriteRenderer>().sprite = item.itemImage;
        } 
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
    }
}
