using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    Transform tr;
    GameObject dustMenu;
    ActionContoller actionContoller;
    public float Energy = 20f;
    public float Exp = 10f;
    void Awake()
    {
        tr = GetComponent<Transform>();
        dustMenu = transform.GetChild(0).gameObject;
        actionContoller = Camera.main.GetComponent<ActionContoller>();
    }

    public void ShowMenu(bool isopen)
    {
        dustMenu.SetActive(isopen);
    }


    public void ClickRemoveDust()
    {
        actionContoller.PleaseRemoveDust(tr);
        ShowMenu(false);
    }
}
