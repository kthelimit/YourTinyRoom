﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform Tr;
    private CanvasGroup canvasGroup;
    public Camera uiCamera;
    public float height = -90f; //위치 이동할때 기준이 될 부분


    void Start()
    {
        Tr = transform.parent.GetComponent<RectTransform>();
        canvasGroup = transform.parent.GetComponent<CanvasGroup>();
        uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }


    //드래그 시작했을때
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    //드래그이벤트
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(Tr.position);
        var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y + height, 100f);// z값을 Plane Distance 값을 줘야 합니다!!
        Tr.position = uiCamera.ScreenToWorldPoint(screenPoint); // 그리고 좌표 변환을 하면 끝!
                                                                       // Tr.position = Input.mousePosition + new Vector3(0,height,0);
    }

    //드래그 끝났을때
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;        
    }
}
