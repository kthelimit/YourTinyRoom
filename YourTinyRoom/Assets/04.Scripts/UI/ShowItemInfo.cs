using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowItemInfo : MonoBehaviour
{
    public GameObject ClosePanel;
    public Item item = null;
    [SerializeField]
    private Text text_count;
    [SerializeField]
    private GameObject go_CountImage;
    private Image itemImage;
    private Text itemName;
    private Text itemDesc;
    public static ShowItemInfo showItemInfo;
    private CanvasGroup cg;
    public bool isShowing = false;
    Inventory inventory;
    public Button UseBtn;
    public Button ThrowBtn;
    public GameObject tdPanel;
    private int curCount;
    public int throwQuantity=1;
    public Slider throwSlider;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<Text>();
        itemDesc = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        ClosePanel.SetActive(false);

        showItemInfo = this;
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
        tdPanel.SetActive(false);
    }
    public void ShowInfo(Item _item, int _itemCount)
    {
        if (_itemCount == 0) return;
        if (!isShowing)
        {
            ClosePanel.SetActive(true);
            isShowing = true;
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
            item = _item;
            itemImage.sprite = _item.itemImage;
            itemImage.type = 0;
            itemImage.preserveAspect = true;
            itemName.text = _item.ItemName;
            itemDesc.text = _item.ItemDesc;
            curCount = _itemCount;
            if (item.itemType != Item.ItemType.COLLECT)
            {
                go_CountImage.SetActive(true);
                text_count.text = curCount.ToString();
                UseBtn.interactable = true;
                ThrowBtn.interactable = true;
            }
            else
            {
                text_count.text = "0";
                go_CountImage.SetActive(false);
                UseBtn.interactable = false;
                ThrowBtn.interactable = false;
            }

        }
        else
        {
            CloseItemInfo();
        }
    }

    public void CloseItemInfo()
    {
        ClosePanel.SetActive(false);
        isShowing = false;
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void UseItem()
    {
        inventory.AcquireItem(item, -1);
        curCount--;
        text_count.text = curCount.ToString();
        //효과 발휘하기(추후 추가)
    }

    public void OpenThrowDetail()
    {
        tdPanel.SetActive(true);

        throwSlider.minValue = 1f;
        throwSlider.maxValue = curCount;
        tdPanel.transform.GetChild(0).GetComponent<Text>().text = throwQuantity.ToString() + "개";
    }
    public void CloseThrowDetail()
    {
        tdPanel.SetActive(false);
    }

    public void SetQuantity(float sliderValue)
    {                
        throwQuantity = (int)sliderValue;
        tdPanel.transform.GetChild(0).GetComponent<Text>().text = throwQuantity.ToString() + "개";
    }


    public void ThrowItem()
    {
        inventory.AcquireItem(item, -throwQuantity);
        curCount-=throwQuantity;
        text_count.text = curCount.ToString();
        tdPanel.SetActive(false);
    }

}
