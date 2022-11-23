using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionContoller : MonoBehaviour
{
    private Camera cam;
    Vector3 MousePosition;
    Inventory inventory;
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
    }

    void Update()
    {
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
                Debug.Log(hit.transform.GetComponent<ItemInfo>().item.ItemName);
                inventory.AcquireItem(hit.transform.GetComponent<ItemInfo>().item);                
                Destroy(hit.transform.gameObject,0.5f);
                //obj.transform.SetParent(canvasUI);

                //잘 안되네.. 좀 더 고민해보자
                GameObject obj = Instantiate(getEffectPrefab, hit.transform.position, hit.transform.rotation);

                obj.GetComponentInChildren<Image>().sprite= obj.GetComponent<Item>().itemImage;
                obj.GetComponentInChildren<Text>().text = 1.ToString();
                
            }
            if (hit.collider.tag == "CROP")
            {
                Crop crop = hit.collider.GetComponent<Crop>();
                if (crop.isComplete == true)
                {
                    Debug.Log(hit.transform.GetComponent<ItemInfo>().item.ItemName);
                    inventory.AcquireItem(hit.transform.GetComponent<ItemInfo>().item, crop.quantity);
                    GameManager.gameManager.IncreaseExp(10f);
                    Destroy(hit.transform.gameObject);
                }
                else
                {
                    crop.ShowLeftTime();
                }

            }

        }
    }



}
