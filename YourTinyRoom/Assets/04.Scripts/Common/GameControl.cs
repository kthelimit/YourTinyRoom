using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameControl : MonoBehaviour
{
    public GameObject PauseImg;
    public RectTransform SettingMenu;
    public CanvasGroup InventoryCG;
    public CanvasGroup CollectionCG;
    public RectTransform ShopMenu;
    public CanvasGroup CustomizeCG;
    public CanvasGroup QuestListCG;
    public CanvasGroup GridBuildCG;
    public CanvasGroup AllCanvasCG;
    public Tilemap mainTileMap;
    public GameObject CharacterVisit;
    public GameObject CharacterVisitSub;
    private bool isCharaVSubOpen = false;
    private Vector3 charaVTarget=Vector3.zero;
    public bool isEditable = false;

    void Awake()
    {
        InventoryCG.alpha = 0;
        InventoryCG.blocksRaycasts = false;
        InventoryCG.interactable = false;
        CollectionCG.alpha = 0;
        CollectionCG.blocksRaycasts = false;
        CollectionCG.interactable = false;
        charaVTarget = CharacterVisitSub.transform.position;
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


    public void Shop()
    {
        if (ShopMenu.gameObject.activeInHierarchy == false)
        {
            ShopMenu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            ShopMenu.gameObject.SetActive(false);
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

    public void OpenQuestList(bool isopen)
    {
        if (QuestListCG.alpha == 1f)
            isopen = false;

        QuestListCG.alpha = isopen ? 1.0f : 0.0f;
        QuestListCG.blocksRaycasts = isopen;
        QuestListCG.interactable = isopen;
    }

    public void OpenCollection(bool isopen)
    {
        if (CollectionCG.alpha == 1f)
            isopen = false;
        CollectionCG.alpha = isopen ? 1.0f : 0.0f;
        CollectionCG.blocksRaycasts = isopen;
        CollectionCG.interactable = isopen;
    }

    public void OpenCustomize(bool isopen)
    {
        if (CustomizeCG.alpha == 1f)
            isopen = false;
        CustomizeCG.alpha = isopen ? 1.0f : 0.0f;
        CustomizeCG.blocksRaycasts = isopen;
        CustomizeCG.interactable = isopen;
    }

    public void OpenGridBuildSystem(bool isopen)
    {
        if (GridBuildingSystem.gbSystem.isOnMouse) return;
        if (GridBuildCG.alpha == 1f)
            isopen = false;
        isEditable = isopen;
        GridBuildCG.alpha = isopen ? 1.0f : 0.0f;
        GridBuildCG.blocksRaycasts = isopen;
        GridBuildCG.interactable = isopen;
        Color _color = mainTileMap.color;
        _color.a = isopen ? 0.3f : 0.0f;
        mainTileMap.color = _color;
        AllCanvasCG.alpha = isopen ? 0.0f : 1.0f;
        AllCanvasCG.blocksRaycasts = !isopen;
        AllCanvasCG.interactable = !isopen;
        if(isopen)
        {
            Camera.main.cullingMask = 1<<8;
        }
        else
        {
            Camera.main.cullingMask = ~(1 << 5);
        }

    }

    public void OpenCharacterVisit(bool isopen)
    {
        CharacterVisit.SetActive(isopen);
    }

    public void OpenCharacterVisitSub()
    {

        if (!isCharaVSubOpen)
        {
            isCharaVSubOpen = true;
            charaVTarget = new Vector3(-5.5f, 1.4f, 96.5f);
            StartCoroutine("MoveCharaVisitSubPanel");

        }
        else
        {
            isCharaVSubOpen = false;
            charaVTarget = new Vector3(-9.6f, 1.4f, 96.5f);
            StartCoroutine("MoveCharaVisitSubPanel");
        }
    }

    IEnumerator MoveCharaVisitSubPanel()
    {
        while (true)
        {
            yield return null;
            CharacterVisitSub.transform.position=Vector3.MoveTowards(CharacterVisitSub.transform.position, charaVTarget, 0.1f);
            if (Vector3.Distance(CharacterVisitSub.transform.position, charaVTarget) <= 0.1f)
            {
                CharacterVisitSub.transform.position = charaVTarget;
                break; 
            }
        }
    }
}
