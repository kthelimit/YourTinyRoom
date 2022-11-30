﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectSlot : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    public int itemNumber;
    public Button button;
    public bool isCollected; //수집한 적이 있는지 여부 등록
    public ShowCollectInfo showCInfo;

    public void SetItem(Item _item)
    {
        item = _item;
        itemNumber = item.ItemNumber;
        itemImage.sprite = item.itemImage;
    }

    public void CheckCollected()
    {
        if (isCollected)
        {
            itemImage.color = Color.white;
            button.interactable = true;
        }
        else
        {
            itemImage.color = Color.grey;
            //button.interactable = false;
        }
    }

    public void OnClicked()
    {
        showCInfo.ShowInfo(item);
    }



}
