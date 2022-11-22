using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject SlotsParent;
    [SerializeField]
    private Slot[] slots;

    void Awake()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
    }


    public void AcquireItem(Item _item, int _count=1)
    {
        if(Item.ItemType.COLLECT!=_item.itemType)
        {
            for(int i=0;i<slots.Length;i++)
            {
                if (slots[i].item == null)
                    break;
                if (slots[i].item.ItemName==_item.ItemName)
                {
                    slots[i].SetSlotCount(_count);
                    return;
                }
            }
        }

        for(int i =0; i<slots.Length;i++)
        {
            if(slots[i].item==null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }


    }


}
