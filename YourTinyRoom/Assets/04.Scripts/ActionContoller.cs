using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContoller : MonoBehaviour
{
    private Camera cam;
    Vector3 MousePosition;
    private Inventory inventory;
    private readonly string hashITEM = "ITEM";

    void Start()
    {
        cam = GetComponent<Camera>();
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
                Debug.Log(hit.transform.name);
                Item _item = hit.transform.GetComponent<ItemInfo>().item;
                inventory.AcquireItem(_item);
                Destroy(hit.transform.gameObject);
                
            }
        }

    }

  
}
