using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Collections : MonoBehaviour
{

    public GameObject SlotsParent;
    [SerializeField]
    private CollectSlot[] slots;
    [SerializeField]
    private Item[] Items;
    private List<Item> ItemList;
    [SerializeField]
    private Item.ItemType itemCategory = Item.ItemType.USED;
    public Transform buttonsParent;
    public Button[] categoryBtns;
    public float prevXPos;
    void Awake()
    {
        slots = SlotsParent.GetComponentsInChildren<CollectSlot>();
        Items = Resources.LoadAll<Item>("Item");
        ItemList = new List<Item>();
        categoryBtns = buttonsParent.GetComponentsInChildren<Button>();
        prevXPos = categoryBtns[0].transform.position.x;


        foreach (Item item in Items)
        {
            if (item.itemType != Item.ItemType.USED)
                ItemList.Add(item);
        }    
        AssignSlot();
        CheckItem();
    }

    void AssignSlot() // 컬렉션 내의 슬롯에 아이템 번호대로 아이템 인포를 배정함
    {
        int slotIdx = 0;
        ClearSlot();
        for (int i=0;i<ItemList.Count;i++)
        {
            if (ItemList[i].itemType == itemCategory)
            {
                slots[slotIdx].SetItem(ItemList[i]);
                slotIdx++;
            }
        }
    }
    void ClearSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = null;
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

    private void ChangeBtnPos(int type)
    {
        foreach (Button _btn in categoryBtns)
        {
            Vector3 PrevPos = new Vector3(prevXPos, _btn.transform.position.y, _btn.transform.position.z);
            _btn.transform.position = PrevPos;
            _btn.interactable = true;
        }
        switch (type)
        {
            case 0:
                categoryBtns[0].transform.position -= new Vector3(0.5f, 0f, 0f);
                categoryBtns[0].interactable = false;
                break;
            case 1:
                categoryBtns[4].transform.position -= new Vector3(0.5f, 0f, 0f);
                categoryBtns[4].interactable = false;
                break;
            case 2:
                categoryBtns[3].transform.position -= new Vector3(0.5f, 0f, 0f);
                categoryBtns[3].interactable = false;
                break;
            case 3:
                break;
            case 4:
                categoryBtns[1].transform.position -= new Vector3(0.5f, 0f, 0f);
                categoryBtns[1].interactable = false;
                break;
            case 5:
                categoryBtns[2].transform.position -= new Vector3(0.5f, 0f, 0f);
                categoryBtns[2].interactable = false;
                break;
        }
    }

    public void ChangeCategory(int type)
    {
        ChangeBtnPos(type);
        switch (type)
        {
            case 0:
                itemCategory = Item.ItemType.USED;
                break;
            case 1:
                itemCategory = Item.ItemType.COLLECT;
                break;
            case 2:
                itemCategory = Item.ItemType.INGREDIENT;
                break;
            case 3:
                itemCategory = Item.ItemType.CUSTOM;
                break;
            case 4:
                itemCategory = Item.ItemType.FURNITURE;
                break;
            case 5:
                itemCategory = Item.ItemType.GIFT;
                break;
        }
        AssignSlot();


    }

}
