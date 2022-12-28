using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Shop : MonoBehaviour
{
    public GameObject SlotsParent;
    private ShopItem[] shopItems;
    private GameObject ShopItemPrefab;
    public Item[] itemList;
    public Transform buttonsParent;
    public Button[] categoryBtns;
    public Color selectedColor;
    public Color notSelectedColor;
    private Item.ItemType itemCategory = Item.ItemType.USED;

    void Awake()
    {
        shopItems = SlotsParent.GetComponentsInChildren<ShopItem>();
        categoryBtns = buttonsParent.GetComponentsInChildren<Button>();
        ShopItemPrefab=(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/ShopItem.prefab", typeof(GameObject));
        AssignSlot();
    }
    void AssignSlot() // 컬렉션 내의 슬롯에 아이템 번호대로 아이템 인포를 배정함
    {
        ClearSlot();
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].itemType == itemCategory)
            {
                GameObject obj= Instantiate(ShopItemPrefab, SlotsParent.transform);
                obj.GetComponent<ShopItem>().SetItem(itemList[i]);
            }
        }
    }
    void ClearSlot()
    {
        shopItems = SlotsParent.GetComponentsInChildren<ShopItem>();
        if (shopItems.Length != 0)
        {
            for (int i = 0; i < shopItems.Length; i++)
            {
                shopItems[i].ClearSlot();
            }
        }
    }

    private void ChangeBtnColor(int type)
    {
        foreach(Button btn in categoryBtns)
        {
            btn.GetComponent<Image>().color = notSelectedColor;
        }
        switch (type)
        {
            case 0:
                categoryBtns[0].GetComponent<Image>().color = selectedColor;
                break;
            case 1:
                //컬렉션
                break;
            case 2:
                //재료
                break;
            case 3:
                categoryBtns[2].GetComponent<Image>().color = selectedColor;
                break;
            case 4:
                categoryBtns[1].GetComponent<Image>().color = selectedColor;
                break;
            case 5:
                categoryBtns[3].GetComponent<Image>().color = selectedColor;
                break;
        }
    }

    public void ChangeCategory(int type)
    {
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
        ChangeBtnColor(type);
        AssignSlot();

    }
}
