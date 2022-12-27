using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectSlot : MonoBehaviour
{
    public CollectItem collectItem;
    public Item item;
    public Image itemImage;
    public int itemNumber;
    public Button button;
    public ShowCollectInfo showCInfo;

    private void Awake()
    {
        showCInfo = GameObject.Find("Collection").transform.GetChild(2).GetComponent<ShowCollectInfo>();
    }

    public void SetItem(CollectItem _collectItem)
    {
        collectItem = _collectItem;
        item = collectItem.item;
        itemNumber = item.ItemNumber;
        itemImage.sprite = item.itemImage;
        Color color = itemImage.color;
        color.a = 1f;
        itemImage.color=color;
        itemImage.type = 0;
        itemImage.preserveAspect = true;
        CheckCollected();
    }

    public void CheckCollected()
    {
        if (collectItem != null && collectItem.isCollected) 
        {
            itemImage.color = Color.white;
            button.interactable = true;
        }
        else if(collectItem == null)
        {
            Color color = itemImage.color;
            color.a = 0f;
            itemImage.color = color;

        }
        else
        {
            itemImage.color = Color.black;
            button.interactable = false;
        }
    }

    public void OnClicked()
    {
        showCInfo.ShowInfo(item);
    }

    public void ClearSlot()
    {
        collectItem = null;
        item = null;
        itemImage.sprite = null;
        itemNumber = 0;
        Color color = itemImage.color;
        color.a = 0f;
        itemImage.color = color;
        button.interactable = false;
    }


}
