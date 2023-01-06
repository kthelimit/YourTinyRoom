using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEditor;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem gbSystem;

    public GridLayout gridLayout;
    public Tilemap MainTileMap;
    public Tilemap TempTileMap;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    public Building temp;
    private Item tempItem;
    private Vector3 prevPos;
    private BoundsInt prevArea;
    public Transform spawnPoint;

    public bool isOnMouse = false;
    public Inventory inventory;

    #region Unity Methods

    private void Awake()
    {
        gbSystem = this;
    }

    private void Start()
    {
        //string tilePath = @"09.Tilemap\";
        tileBases.Add(TileType.EMPTY, null);
        tileBases.Add(TileType.WHITE, (TileBase)AssetDatabase.LoadAssetAtPath("Assets/09.Tilemap/white.asset", typeof(TileBase)));
        tileBases.Add(TileType.GREEN, (TileBase)AssetDatabase.LoadAssetAtPath("Assets/09.Tilemap/green.asset", typeof(TileBase)));
        tileBases.Add(TileType.RED, (TileBase)AssetDatabase.LoadAssetAtPath("Assets/09.Tilemap/red.asset", typeof(TileBase)));
    }

    private void Update()
    {
        if (!temp)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            //if(EventSystem.current.IsPointerOverGameObject(0))
            //{
            //    return;           
            //}
            if (!temp.Placed)
            {
                Vector2 touchPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPos = gridLayout.LocalToCell((Vector3)touchPos);
                if (prevPos!=(Vector3)cellPos&&!temp.isShow)
                {
                    temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPosition: (Vector3)cellPos + new Vector3(x: .5f, y: .5f, z: 0f));
                    prevPos = (Vector3)cellPos;
                    FollowBuilding();
                    temp.CanBePlaced();
                }
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if (temp.Placed) return;
            ReturnToInventory();

        }
    }

    public void ReturnToInventory()
    {
        inventory.AcquireItem(tempItem, 1);
        DeleteObject();
    }

    #endregion

    public void DeleteObject()
    {
        ClearArea();
        Destroy(temp.gameObject);
        isOnMouse = false;
    }



    #region Tilemap Management
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, z: 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }
    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i=0; i<arr.Length;i++)
        {
            arr[i] = tileBases[type];
        }
    }


    #endregion





    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        if (isOnMouse) return;
        temp = Instantiate(building, spawnPoint.position, Quaternion.identity).GetComponent<Building>();
        tempItem = temp.GetComponent<ItemInfo>().item;
        FollowBuilding();
        isOnMouse = true;
        inventory.AcquireItem(tempItem, -1);
    }

    //재배치
    public void RearrangeBuilding(GameObject building)
    {
        temp = building.GetComponent<Building>();
        temp.Rearrange();
        ClearArea(temp.area);
        tempItem = temp.GetComponent<ItemInfo>().item;
        FollowBuilding();
        isOnMouse = true;
    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x*prevArea.size.y*prevArea.size.z];
        FillTiles(toClear, TileType.EMPTY);
        TempTileMap.SetTilesBlock(prevArea, toClear);
    }

    //재배치용 클리어에리어
    public void ClearArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.WHITE, MainTileMap);
    }

    private void FollowBuilding()
    {
        ClearArea();
        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;
        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTileMap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i= 0; i<baseArray.Length;i++)
        {
            if(baseArray[i]==tileBases[TileType.WHITE])
            {
                tileArray[i] = tileBases[TileType.GREEN];
            }
            else
            {
                FillTiles(tileArray, TileType.RED);
                break;
            }
               
        }
        TempTileMap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, MainTileMap);
        foreach(var b in baseArray)
        {
            if(b != tileBases[TileType.WHITE])
            {
                Debug.Log("여기에 둘 수 없습니다.");
                return false;
            }
        }
        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.EMPTY, TempTileMap);
        SetTilesBlock(area, TileType.GREEN, MainTileMap);

    }


    #endregion

    public enum TileType
    {
        EMPTY,
        WHITE,
        RED,
        GREEN
    };

}
