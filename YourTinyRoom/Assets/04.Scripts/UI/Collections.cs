using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Collections : MonoBehaviour
{

    public GameObject SlotsParent;
    [SerializeField]
    private CollectSlot[] slots;
    [SerializeField]
    private Item[] Items;
    private List<Item> ItemList;
    void Awake()
    {
        slots = SlotsParent.GetComponentsInChildren<CollectSlot>();
        Items = Resources.LoadAll<Item>("Item");
        ItemList = new List<Item>();
        foreach(Item item in Items)
        {
            if (item.itemType != Item.ItemType.USED)
                ItemList.Add(item);
        }    
        AssignSlot();
        CheckItem();
    }

    void AssignSlot() // 컬렉션 내의 슬롯에 아이템 번호대로 아이템 인포를 배정함
    {
        for(int i=0;i<ItemList.Count;i++)
        {
             slots[i].SetItem(ItemList[i]);
        }
    }

    public void Collect(Item _item)
    {
        int num = _item.ItemNumber;
        for(int i=0; i<slots.Length;i++)
        {
            if (slots[i].itemNumber == num)
            {
                slots[i].isCollected = true;
                CheckItem();
                break;
            }
        }
    }

    void CheckItem() //구성원들의 획득 여부를 체크함
    {
        foreach (CollectSlot _slot in slots)
        {
            _slot.CheckCollected();
        }
    }


}
