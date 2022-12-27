using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


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
    public float prevXPos;
    public GameObject FSlotParent;
    public Slot[] fSlots;
    GameObject FslotPrefab;
    GameObject furniturePrefab;

    void Awake()
    {
        FslotPrefab= (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/FSlot.prefab", typeof(GameObject));
        furniturePrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/Furniture/furniture.prefab", typeof(GameObject));
        slots = SlotsParent.GetComponentsInChildren<Slot>();
        fSlots= FSlotParent.GetComponentsInChildren<Slot>();
        itemsInInventory =new List<ItemInInventory>();
        categoryBtns = buttonsParent.GetComponentsInChildren<Button>();
        prevXPos = categoryBtns[0].transform.position.x;
        ChangeBtnPos(0);
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
                    if (_item.itemType != Item.ItemType.FURNITURE)
                        ShowItemList();
                    else
                        ShowFurnitureList();
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
        if(_item.itemType!=Item.ItemType.FURNITURE)
            ShowItemList();
        else
            ShowFurnitureList();
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
        ClearAllSlot();
        for (int i=0; i<itemsInInventory.Count;i++)
        {
            if (itemsInInventory[i].item.itemType == itemCategory && itemsInInventory[i].itemCount > 0) 
            {              
                slots[slotIdx].AddItem(itemsInInventory[i].item, itemsInInventory[i].itemCount);
                slotIdx++;
            }
        }
    }

    public void ShowFurnitureList()
    {
        int slotIdx = 0;
        ClearAllFSlot();
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            if (itemsInInventory[i].item.itemType == Item.ItemType.FURNITURE && itemsInInventory[i].itemCount > 0)
            {
                furniturePrefab.GetComponent<ItemInfo>().item = itemsInInventory[i].item;
                if(fSlots.Length==slotIdx)
                {
                    Instantiate(FslotPrefab, FSlotParent.transform);
                }
                fSlots[slotIdx].AddItem(itemsInInventory[i].item, itemsInInventory[i].itemCount);
                fSlots[slotIdx].GetComponentInChildren<Button>().onClick.AddListener(()=>GridBuildingSystem.gbSystem.InitializeWithBuilding(furniturePrefab));
                slotIdx++;
            }
        }
    }

    private void ClearAllSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].itemCount = 0;
            slots[i].SetSlotCount(0);
        }
    }
    private void ClearAllFSlot()
    {
        for (int i = 0; i < fSlots.Length; i++)
        {
            fSlots[i].itemCount = 0;
            fSlots[i].SetSlotCount(0);
            fSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
    }
    private void ChangeBtnPos(int type)
    {
        foreach (Button _btn in categoryBtns)
        {
            Vector3 PrevPos= new Vector3(prevXPos,_btn.transform.position.y, _btn.transform.position.z);
            _btn.transform.position = PrevPos;
            _btn.interactable = true;
        }
        switch (type)
        {
            case 0:
                categoryBtns[0].transform.position-= new Vector3(0.5f,0f,0f);
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

        ShowItemList();
        ShowFurnitureList();
    }
}
