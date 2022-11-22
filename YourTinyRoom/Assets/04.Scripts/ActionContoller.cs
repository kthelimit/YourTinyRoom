using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContoller : MonoBehaviour
{
    private Camera cam;
    Vector3 MousePosition;
    Inventory inventory;

    private readonly string hashITEM = "ITEM";

    void Start()
    {
        cam = GetComponent<Camera>();
        inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = cam.ScreenToWorldPoint(MousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition,-Vector2.up);     
            Debug.DrawRay(MousePosition, transform.forward * 20, Color.red, 0.3f);
            if(hit.collider.tag=="ITEM")
            {
                Debug.Log(hit.transform.GetComponent<ItemInfo>().item.ItemName);
                inventory.AcquireItem(hit.transform.GetComponent<ItemInfo>().item);
                Destroy(hit.transform.gameObject);
                
            }
            if (hit.collider.tag == "CROP")
            {
                Crop crop = hit.collider.GetComponent<Crop>();
                if (crop.isComplete == true)
                {
                    Debug.Log(hit.transform.GetComponent<ItemInfo>().item.ItemName);
                    inventory.AcquireItem(hit.transform.GetComponent<ItemInfo>().item, crop.quantity);
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
