using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;    

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
        GridBuildingSystem.gbSystem.TakeArea(areaTemp);
    }



    #endregion
}
