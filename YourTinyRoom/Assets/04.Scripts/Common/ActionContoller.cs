using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionContoller : MonoBehaviour
{
    private Camera cam;
    Vector3 MousePosition;
    Inventory inventory;
    Collections collections;
    [SerializeField]
    Transform canvasUI;
    private GameObject getEffectPrefab;
    public ItemInfo Exp;
    public ItemInfo Gold;
    public ItemInfo Crystal;
    private readonly string hashItem = "ITEM";
    private readonly string hashCrop = "CROP";
    private readonly string hashFurniture = "FURNITURE";

    void Start()
    {
        canvasUI = GameObject.Find("Canvas-UI").transform;
        cam = GetComponent<Camera>();
        getEffectPrefab = Resources.Load<GameObject>("GetEffect");
        inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
        collections = GameObject.Find("Collection").transform.GetComponent<Collections>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            ClickItem();
        }
    }

    private void ClickItem()
    {
        MousePosition = Input.mousePosition;
        MousePosition = cam.ScreenToWorldPoint(MousePosition);

        RaycastHit2D hit = Physics2D.Raycast(MousePosition, -Vector2.up);
        Debug.DrawRay(MousePosition, transform.forward * 20, Color.red, 0.3f);
        if (hit.collider.tag == hashItem)
        {
            GetItem(hit);
        }
        else if (hit.collider.tag == hashCrop)
        {
            GetCrop(hit);

        }
        else if(hit.collider.tag == hashFurniture)
        {
            GetFurniture(hit);
        }

    }

    private void GetCrop(RaycastHit2D hit)
    {
        Crop crop = hit.collider.GetComponent<Crop>();
        if (crop.isComplete == true)
        {
            Item item = hit.transform.GetComponent<ItemInfo>().item;
            inventory.AcquireItem(item, crop.quantity);
            collections.Collect(item);
            GameManager.gameManager.IncreaseExp(crop.exp);
            Destroy(hit.transform.gameObject);
            ShowGetEffect(hit.transform, item, crop.quantity);
            Transform tr2 = hit.transform;
            tr2.position = new Vector3(tr2.position.x, tr2.position.y-0.6f, tr2.position.z);
            ShowGetEffect(tr2, Exp.item, (int)crop.exp);
            Building cropPlace = crop.GetComponent<Building>();
            GridBuildingSystem.gbSystem.ClearArea(cropPlace.area);
        }
        else
        {            
            crop.ShowLeftTime();
            Building cropPlace= crop.GetComponent<Building>();
            if(!cropPlace.Placed)
            {
                if (cropPlace.CanBePlaced())
                {
                    cropPlace.Place();
                }
            }
        }
    }
    private void GetItem(RaycastHit2D hit)
    {
        Item item = hit.transform.GetComponent<ItemInfo>().item;
        inventory.AcquireItem(item);
        collections.Collect(item);
        Destroy(hit.transform.gameObject, 0.1f);
        ShowGetEffect(hit.transform, item);
    }

    private void GetFurniture(RaycastHit2D hit)
    {
        Building furniture = hit.transform.GetComponent<Building>();
        if(!furniture.Placed)
        {
            if (furniture.CanBePlaced())
            {
                furniture.PlaceFurniture();
            }
        }
        //inventory.AcquireItem(item);
        //collections.Collect(item);
        //Destroy(hit.transform.gameObject, 0.1f);
        //ShowGetEffect(hit.transform, item);
    }


    private void ShowGetEffect(Transform tr, Item item, int quantity = 1)
    {
        GameObject obj = Instantiate(getEffectPrefab, tr.position, tr.rotation);
        obj.GetComponent<CsScore>().ChangeInfo(item, quantity);
    }
}
