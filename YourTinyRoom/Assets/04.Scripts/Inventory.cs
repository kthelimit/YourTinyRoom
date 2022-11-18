using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count=1)
    {
        if(Item.ItemType.COLLECT!=_item.itemType)
        {
            for(int i=0;i<slots.Length;i++)
            {
                if(slots[i].item.ItemName==_item.ItemName)
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
