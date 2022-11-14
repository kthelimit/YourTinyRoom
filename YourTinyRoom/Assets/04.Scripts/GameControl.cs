using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject PauseImg;
    public RectTransform SettingMenu;

    void Start()
    {
        PauseImg = GameObject.Find("Canvas-UI").transform.GetChild(6).gameObject;
        SettingMenu = PauseImg.transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Application.platform==RuntimePlatform.WindowsEditor) //윈도우 버전에서만 ESC에 반응
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
    }

    public void Pause()
    {
        if(PauseImg.gameObject.activeInHierarchy==false)
        {
            if (SettingMenu.gameObject.activeInHierarchy == false)
            {
                SettingMenu.gameObject.SetActive(true);
            }
            PauseImg.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PauseImg.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

    }
}
