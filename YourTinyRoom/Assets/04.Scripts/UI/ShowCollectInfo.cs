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

    private void Awake()
    {
        itemImage.sprite = null;
        itemName.text = " ";
        itemDesc.text = " ";
    }

    public void ShowInfo(Item _item)
    {
        item = _item;
        itemImage.sprite = _item.itemImage;
        itemImage.type = 0;
        itemImage.preserveAspect = true;
        itemName.text = _item.ItemName;
        itemDesc.text = _item.ItemDesc;
    }

}
