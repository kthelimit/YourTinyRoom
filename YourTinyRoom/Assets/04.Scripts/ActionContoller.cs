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

    private readonly string hashITEM = "ITEM";

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
        ClickItem();

    }

    private void ClickItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = cam.ScreenToWorldPoint(MousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, -Vector2.up);
            Debug.DrawRay(MousePosition, transform.forward * 20, Color.red, 0.3f);
            if (hit.collider.tag == "ITEM")
            {
                GetItem(hit);
            }
            if (hit.collider.tag == "CROP")
            {
                GetCrop(hit);

            }

        }
    }

    private void GetCrop(RaycastHit2D hit)
    {
        Crop crop = hit.collider.GetComponent<Crop>();
        if (crop.isComplete == true)
        {
            ItemInfo itemInfo = hit.transform.GetComponent<ItemInfo>();
            inventory.AcquireItem(itemInfo.item, crop.quantity);
            collections.Collect(itemInfo.item);
            GameManager.gameManager.IncreaseExp(crop.exp);
            Destroy(hit.transform.gameObject);
            GameObject obj = Instantiate(getEffectPrefab, hit.transform.position, hit.transform.rotation);
            obj.GetComponent<CsScore>().ChangeInfo(itemInfo, crop.quantity);
        }
        else
        {
            crop.ShowLeftTime();
        }
    }

    private void GetItem(RaycastHit2D hit)
    {
        ItemInfo itemInfo = hit.transform.GetComponent<ItemInfo>();
        inventory.AcquireItem(itemInfo.item);
        collections.Collect(itemInfo.item);
        Destroy(hit.transform.gameObject, 0.1f);
        GameObject obj = Instantiate(getEffectPrefab, hit.transform.position, hit.transform.rotation);
        obj.GetComponent<CsScore>().ChangeInfo(itemInfo);
    }
}
