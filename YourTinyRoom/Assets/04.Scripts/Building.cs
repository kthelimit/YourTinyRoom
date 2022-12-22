using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private const int IsometricRangePerYUnit = 100;

    #region Build methods
    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.gbSystem.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(GridBuildingSystem.gbSystem.CanTakeArea(areaTemp))
        {
            return true;           
        }
        return false;

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
        renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
        GridBuildingSystem.gbSystem.TakeArea(areaTemp);
        GridBuildingSystem.gbSystem.isOnMouse = false;
      
    }


    #endregion
}
