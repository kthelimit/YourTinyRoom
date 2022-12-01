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

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
       // go_CountImage = transform.GetChild(0).GetChild(0).GetComponent<GameObject>();
       // text_count = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        itemName = transform.GetChild(1).GetComponent<Text>();
        itemDesc = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        ClosePanel.SetActive(false);

        showItemInfo = this;
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
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
            itemName.text = _item.ItemName;
            itemDesc.text = _item.ItemDesc;
            if (item.itemType != Item.ItemType.COLLECT)
            {
                go_CountImage.SetActive(true);
                text_count.text = _itemCount.ToString();
            }
            else
            {
                text_count.text = "0";
                go_CountImage.SetActive(false);
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
}
