using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DataInfo;
public class Collections : MonoBehaviour
{

    public GameObject SlotsParent;
    [SerializeField]
    private CollectSlot[] slots;
    [SerializeField]
    private Item[] Items;
    private List<Item> ItemList;
    private List<CollectItem> CollectList;
    [SerializeField]
    private Item.ItemType itemCategory = Item.ItemType.USED;
    public Transform buttonsParent;
    public Button[] categoryBtns;
    public float prevXPos;
    private GameObject slotPrefab;
    void Awake()
    {
        slots = SlotsParent.GetComponentsInChildren<CollectSlot>();
        Items = Resources.LoadAll<Item>("Item");
        ItemList = new List<Item>();        
        categoryBtns = buttonsParent.GetComponentsInChildren<Button>();
        prevXPos = categoryBtns[0].transform.position.x;
        slotPrefab = Resources.Load<GameObject>("Prefabs/CollectionSlot");
            //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/CollectionSlot.prefab", typeof(GameObject));
        foreach (Item item in Items)
        {
            if (item.ItemNumber != 0)
                ItemList.Add(item);
        }
        CollectList = new List<CollectItem>();
        foreach (Item item in ItemList)
        {
            CollectItem collectItem = new CollectItem();
            collectItem.item = item;
            collectItem.ItemNumber = item.ItemNumber;
            CollectList.Add(collectItem);
        }
        ChangeCategory(0);
        CheckItem();
    }

    public void LoadCollections(List<CollectItem> _list)
    {
        if (_list.Count == 0) return;

        for (int i = 0; i < CollectList.Count; i++)
        {
            CollectList[i].isCollected = _list[i].isCollected;
        }
        CheckItem();   
    }

    public List<CollectItem> SaveCollections()
    {
        return CollectList;
    }
    void AssignSlot() // 컬렉션 내의 슬롯에 아이템 번호대로 아이템 인포를 배정함
    {
        int slotIdx = 0;
        ClearSlot();
        for (int i=0;i< CollectList.Count;i++)
        {
            if (CollectList[i].item.itemType == itemCategory)
            {
                if(slots.Length==slotIdx)
                {
                    Instantiate(slotPrefab, SlotsParent.transform);
                    slots = SlotsParent.GetComponentsInChildren<CollectSlot>();
                }
                slots[slotIdx].SetItem(CollectList[i]);
                slotIdx++;
            }
        }
    }
    void ClearSlot()
    {
        slots = SlotsParent.GetComponentsInChildren<CollectSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
        if (slots.Length > 9)
        {
            for (int i = 9; i < slots.Length; i++)
            {
                Destroy(slots[i].gameObject);
            }
        }
        slots = SlotsParent.GetComponentsInChildren<CollectSlot>();
    }

    public void Collect(Item _item)
    {
        int num = _item.ItemNumber;
        for(int i=0; i< CollectList.Count;i++)
        {
            if (CollectList[i].ItemNumber == num&& CollectList[i].item.ItemName==_item.ItemName)
            {
                CollectList[i].isCollected = true;
                AssignSlot();
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
        if(itemCategory!= Item.ItemType.USED)
            prevXPos = categoryBtns[0].transform.position.x;
        else
            prevXPos = categoryBtns[1].transform.position.x;
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
