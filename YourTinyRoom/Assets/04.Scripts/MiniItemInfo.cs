using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniItemInfo : MonoBehaviour
{
    public static MiniItemInfo miniItemInfo;
    public Item item = null;
    private Text itemName;
    private Text itemDesc;
    private CanvasGroup cg; 
    private void Awake()
    {
        miniItemInfo = this;
        cg = GetComponent<CanvasGroup>();
        itemName = transform.GetChild(0).GetComponent<Text>();
        itemDesc= transform.GetChild(1).GetComponent<Text>();
    }
    public void ShowInfo(Item _item)
    {
            cg.alpha = 1;
            item = _item;
            itemName.text = _item.ItemName;
            itemDesc.text = _item.ItemDesc;

    }
    public void CloseItemInfo()
    {
        cg.alpha = 0;
    }
}
