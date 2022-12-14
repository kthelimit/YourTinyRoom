using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    public int quantity=1;
    private void Awake()
    {
        if (GetComponent<SpriteRenderer>() == null) return;
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
    }
}
