using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject SlotsParent;
    [SerializeField]
    private Slot[] slots;
    [SerializeField]
    private List<ItemInInventory> itemsInInventory;
    [SerializeField]
    private Item.ItemType itemCategory=Item.ItemType.USED;
    public Transform buttonsParent;
    public Button[] categoryBtns;

    void Awake()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
        itemsInInventory=new List<ItemInInventory>();
        categoryBtns = buttonsParent.GetComponentsInChildren<Button>();

    }

    public void AcquireItem(Item _item, int _count=1)
    {
        if (Item.ItemType.COLLECT != _item.itemType && Item.ItemType.CUSTOM != _item.itemType) 
        {
            //for(int i=0;i<slots.Length;i++)
            //{
            //    if (slots[i].item == null)
            //        break;
            //    if (slots[i].item.ItemName==_item.ItemName)
            //    {
            //        slots[i].SetSlotCount(_count);
            //        return;
            //    }
            //}
            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                if (itemsInInventory[i].item.ItemName == _item.ItemName)
                {
                    itemsInInventory[i].itemCount += _count;
                    return;
                }
            }
        }

        //for(int i =0; i<slots.Length;i++)
        //{
        //    if(slots[i].item==null)
        //    {
        //        slots[i].AddItem(_item, _count);
        //        return;
        //    }
        //}

        ItemInInventory obj = new ItemInInventory();
        obj.item = _item;
        obj.itemCount = _count;
        itemsInInventory.Add(obj);
        ShowItemList();
    }




    public bool CheckItem(Item _item, int quantity)
    {
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    if (slots[i].item == null)
        //        break;
        //    if (slots[i].item.ItemName == _item.ItemName)
        //    {
        //        if (slots[i].itemCount >= quantity)
        //            return true;
        //    }
        //}

        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            if (itemsInInventory[i].item.ItemName == _item.ItemName)
            {
                if (itemsInInventory[i].itemCount >= quantity)
                    return true;
            }
        }

        return false;
    }

    public void ShowItemList()
    {
        int slotIdx=0;
        ClearSlot();
        for (int i=0; i<itemsInInventory.Count;i++)
        {
            if(itemsInInventory[i].item.itemType==itemCategory)
            {
                slots[slotIdx].AddItem(itemsInInventory[i].item, itemsInInventory[i].itemCount);
                slotIdx++;
            }
        }
    }
    private void ClearSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].itemCount=0;
            slots[i].SetSlotCount(0);
        }
    }

    public void ChangeCategory(int type)
    {
        switch(type)
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

        ShowItemList();
    }
}
