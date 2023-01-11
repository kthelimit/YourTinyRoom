using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FSlot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    [SerializeField]
    private Text text_count;
    [SerializeField]
    private GameObject go_CountImage;
    [SerializeField]
    private GameObject furniturePrefab;

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        itemImage.type = 0;
        itemImage.preserveAspect = true;
        furniturePrefab = Resources.Load<GameObject>($"Prefabs/Furniture/Item{item.ItemNumber.ToString("000")}");

        go_CountImage.SetActive(true);
        text_count.text = itemCount.ToString();

        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    public void ClickFslot()
    {
        GridBuildingSystem.gbSystem.InitializeWithBuilding(furniturePrefab);
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        text_count.text = "0";
        go_CountImage.SetActive(false);
        furniturePrefab = null;
    }

}
