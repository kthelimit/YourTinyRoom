using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject PauseImg;
    public RectTransform SettingMenu;
    public RectTransform QuestListMenu;
    public CanvasGroup InventoryCG;
    public CanvasGroup CollectionCG;

    void Awake()
    {        
        InventoryCG.alpha = 0;
        InventoryCG.blocksRaycasts = false;
        InventoryCG.interactable = false;
        CollectionCG.alpha = 0;
        CollectionCG.blocksRaycasts = false;
        CollectionCG.interactable = false;
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

    public void QuestList()
    {
        if(QuestListMenu.gameObject.activeInHierarchy==false)
        {
            QuestListMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            QuestListMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void OpenInventory(bool isopen)
    {
        if(InventoryCG.alpha==1f)
            isopen = false;
        
        InventoryCG.alpha = isopen?1.0f:0.0f;
        InventoryCG.blocksRaycasts = isopen;
        InventoryCG.interactable = isopen;
    }

    public void OpenCollection(bool isopen)
    {
        if (CollectionCG.alpha == 1f)
            isopen = false;
        CollectionCG.alpha = isopen ? 1.0f : 0.0f;
        CollectionCG.blocksRaycasts = isopen;
        CollectionCG.interactable = isopen;
    }
}
