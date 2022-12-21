using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectSlot : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    public int itemNumber;
    public Button button;
    public bool isCollected=false; //수집한 적이 있는지 여부 등록
    public ShowCollectInfo showCInfo;

    private void Awake()
    {
        showCInfo = GameObject.Find("Collection").transform.GetChild(2).GetComponent<ShowCollectInfo>();
    }

    public void SetItem(Item _item)
    {
        item = _item;
        itemNumber = item.ItemNumber;
        itemImage.sprite = item.itemImage;
        Color color = itemImage.color;
        color.a = 1f;
        itemImage.color=color;
        itemImage.type = 0;
        itemImage.preserveAspect = true;
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
            button.interactable = false;
        }
    }

    public void OnClicked()
    {
        showCInfo.ShowInfo(item);
    }



}
