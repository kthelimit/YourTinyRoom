using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform itemTr;
    private Transform inventoryTr;
    public static GameObject draggingItem = null;
    private Transform itemListTr;
    private CanvasGroup canvasGroup;


    void Start()
    {
        itemTr = GetComponent<Transform>();
        inventoryTr = GameObject.Find("Inventory").GetComponent<Transform>();
        itemListTr = GameObject.Find("ItemList").GetComponent<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //드래그 시작했을때
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventoryTr);
        draggingItem = this.gameObject;
        canvasGroup.blocksRaycasts = false;
    }

    //드래그이벤트
    public void OnDrag(PointerEventData eventData)
    {
        itemTr.position = Input.mousePosition;
    }

    //드래그 끝났을때
    public void OnEndDrag(PointerEventData eventData)
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;
        if (itemTr.parent == inventoryTr)
        {
            itemTr.SetParent(itemListTr.transform);
            //GameManager.gameManager.RemoveItem(GetComponent<ItemInfo>().itemData);
        }
    }
}
