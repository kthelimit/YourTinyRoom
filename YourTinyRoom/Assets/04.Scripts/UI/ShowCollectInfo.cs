using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowCollectInfo : MonoBehaviour
{
    public Item item=null;
    public Image itemImage;
    public Text itemName;
    public Text itemDesc;
    private Color color;

    private void Awake()
    {
        ResetInfo();
    }

    public void ResetInfo()
    {
        itemImage.sprite = null;
        color = itemImage.color;
        color.a = 0f;
        itemImage.color = color;
        itemName.text = " ";
        itemDesc.text = " ";
    }

    public void ShowInfo(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        color = itemImage.color;
        color.a = 1f;
        itemImage.color = color;
        itemImage.type = 0;
        itemImage.preserveAspect = true;
        itemName.text = _item.ItemName;
        itemDesc.text = _item.ItemDesc;
    }

}
