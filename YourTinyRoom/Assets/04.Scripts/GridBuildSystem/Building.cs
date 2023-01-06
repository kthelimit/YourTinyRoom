﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private const int IsometricRangePerYUnit = 100;
    private Transform furnitureFolder;
    private GameObject ControlPanel;
    public bool isShow = false;
    private Button PlaceButton;
    private Button RearrangeButton;
    private Button ReturnButton;
    private Button ReflectButton;
    private bool IsCrop = true;

    private void Awake()
    {
        furnitureFolder = GameObject.Find("FurnitureFolder").transform;
        if (GetComponent<ItemInfo>().itemType == Item.ItemType.FURNITURE)
        {
            IsCrop = false;
            Furniture _item = (Furniture)GetComponent<ItemInfo>().item;
            area = _item.area;
            ControlPanel = transform.GetChild(1).gameObject;
            PlaceButton = ControlPanel.transform.GetChild(1).GetComponent<Button>();
            ReflectButton = ControlPanel.transform.GetChild(2).GetComponent<Button>();
            ReturnButton = ControlPanel.transform.GetChild(3).GetComponent<Button>();
            RearrangeButton = ControlPanel.transform.GetChild(4).GetComponent<Button>();
            PlaceButton.onClick.AddListener(PlaceFurniture);
            ReflectButton.onClick.AddListener(Reflect);
            ReturnButton.onClick.AddListener(ReturnToInventory);
            RearrangeButton.onClick.AddListener(ClickRearrange);
            ShowControlPanel(false);
        }
    }
    #region Build methods
    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.gbSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(GridBuildingSystem.gbSystem.CanTakeArea(areaTemp))
        {            
            if(!IsCrop)
                PlaceButton.interactable=true;
            return true;           
        }
        if (!IsCrop)
            PlaceButton.interactable = false;
        return false;

    }  

    public void ShowControlPanel(bool isOpen)
    {
        ControlPanel.SetActive(isOpen);
        isShow = isOpen;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.gbSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        Renderer renderer = GetComponentInChildren<Renderer>();
        renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
        GetComponentInChildren<Canvas>().sortingOrder= -(int)(transform.position.y * IsometricRangePerYUnit);
        GridBuildingSystem.gbSystem.TakeArea(areaTemp);
        GridBuildingSystem.gbSystem.isOnMouse = false;
        GetComponent<Crop>().StartCoroutine("Timer");
       
    }

    public void PlaceFurniture()
    {
        Vector3Int positionInt = GridBuildingSystem.gbSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        Renderer renderer = GetComponentInChildren<Renderer>();
        renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);//-(int)(transform.position.x);
        GridBuildingSystem.gbSystem.TakeArea(areaTemp);
        GridBuildingSystem.gbSystem.isOnMouse = false;
        this.transform.SetParent(furnitureFolder);
        this.transform.SetAsLastSibling();
        ShowControlPanel(false);
    }

    public void ClickRearrange()
    {
        GridBuildingSystem.gbSystem.RearrangeBuilding(this.gameObject);
    }

    public void ReturnToInventory()
    {
        GridBuildingSystem.gbSystem.ReturnToInventory();
    }

    public void Rearrange()
    {
        Placed = false;
        ShowControlPanel(false);
    }

    public void Reflect()
    {
        Transform SpriteTr= this.transform.GetChild(0).transform;
        if (SpriteTr.eulerAngles.y == 180f)
        {
            SpriteTr.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            SpriteTr.eulerAngles = new Vector3(0f, 180f, 0f);
        }
      
    }


    #endregion



}
