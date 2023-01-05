using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject PauseImg;
    public RectTransform SettingMenu;
    public CanvasGroup InventoryCG;
    public CanvasGroup CollectionCG;
    public CanvasGroup ShopCG;
    public CanvasGroup CustomizeCG;
    public CanvasGroup QuestListCG;
    public CanvasGroup GridBuildCG;
    public CanvasGroup CropCG;
    public CanvasGroup AllCanvasCG;
    public CanvasGroup PhoneCG;
    public Tilemap mainTileMap;
    public CapsuleCollider2D CharacterCollider;
    public GameObject NameEditPanel;
    public GameObject ClosePanel;
    public GameObject CNameEditPanel;
    public GameObject NextDayPanel;
    public GameObject AlarmPanel;


    //캐릭터 방문시 아이콘
    public GameObject CharacterVisit;
    public GameObject CharacterVisitSub;
    private bool isCharaVSubOpen = false;
    private Vector3 charaVTarget = Vector3.zero;
    public bool isEditable = false;

    //장면 이동시
    public GameObject GoFarmObject;
    public GameObject GoRoomObject;
    public GameObject InteriorObject;
    public GameObject CropObject;
    public bool isFarm = false;


    void Awake()
    {
        InventoryCG.alpha = 0;
        InventoryCG.blocksRaycasts = false;
        InventoryCG.interactable = false;
        CollectionCG.alpha = 0;
        CollectionCG.blocksRaycasts = false;
        CollectionCG.interactable = false;
        charaVTarget = CharacterVisitSub.transform.position;
        GoRoom();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor) //윈도우 버전에서만 ESC에 반응
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
    }

    public void Pause()
    {
        if (PauseImg.gameObject.activeInHierarchy == false)
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


    public void Shop(bool isopen)
    {
        if (ShopCG.alpha == 1f)
            isopen = false;

        ShopCG.alpha = isopen ? 1.0f : 0.0f;
        ShopCG.blocksRaycasts = isopen;
        ShopCG.interactable = isopen;
    }


    public void OpenInventory(bool isopen)
    {
        if (InventoryCG.alpha == 1f)
            isopen = false;

        InventoryCG.alpha = isopen ? 1.0f : 0.0f;
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
        CollectionCG.GetComponentInChildren<ShowCollectInfo>().ResetInfo();
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
        CharacterCollider.enabled = !isopen;
        GridBuildCG.alpha = isopen ? 1.0f : 0.0f;
        GridBuildCG.blocksRaycasts = isopen;
        GridBuildCG.interactable = isopen;
        Color _color = mainTileMap.color;
        _color.a = isopen ? 0.3f : 0.0f;
        mainTileMap.color = _color;
        AllCanvasCG.alpha = isopen ? 0.0f : 1.0f;
        AllCanvasCG.blocksRaycasts = !isopen;
        AllCanvasCG.interactable = !isopen;
        if (isopen)
        {
            Camera.main.cullingMask = 1 << 8;
        }
        else
        {
            Camera.main.cullingMask = ~(1 << 5);
        }

    }

    public void OpenCropSystem(bool isopen)
    {
        if (GridBuildingSystem.gbSystem.isOnMouse) return;
        if (CropCG.alpha == 1f)
            isopen = false;
        isEditable = isopen;
        CropCG.alpha = isopen ? 1.0f : 0.0f;
        CropCG.blocksRaycasts = isopen;
        CropCG.interactable = isopen;
        Color _color = mainTileMap.color;
        _color.a = isopen ? 0.3f : 0.0f;
        mainTileMap.color = _color;
        AllCanvasCG.alpha = isopen ? 0.0f : 1.0f;
        AllCanvasCG.blocksRaycasts = !isopen;
        AllCanvasCG.interactable = !isopen;
        if (isopen)
        {
            Camera.main.cullingMask = 1 << 8;
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
            charaVTarget = new Vector3(-5.5f, 1.9f, 96.5f);
            StartCoroutine("MoveCharaVisitSubPanel");

        }
        else
        {
            isCharaVSubOpen = false;
            charaVTarget = new Vector3(-9.6f, 1.9f, 96.5f);
            StartCoroutine("MoveCharaVisitSubPanel");
        }
    }

    IEnumerator MoveCharaVisitSubPanel()
    {
        while (true)
        {
            yield return null;
            CharacterVisitSub.transform.position = Vector3.MoveTowards(CharacterVisitSub.transform.position, charaVTarget, 0.1f);
            if (Vector3.Distance(CharacterVisitSub.transform.position, charaVTarget) <= 0.1f)
            {
                CharacterVisitSub.transform.position = charaVTarget;
                break;
            }
        }
    }


    public void GoFarm()
    {
        isFarm = true;
        GoFarmObject.SetActive(false);
        GoRoomObject.SetActive(true);
        CropObject.SetActive(true);
        InteriorObject.SetActive(false);

    }

    public void GoRoom()
    {
        isFarm = false;
        GoFarmObject.SetActive(true);
        GoRoomObject.SetActive(false);
        CropObject.SetActive(false);
        InteriorObject.SetActive(true);
    }

    public void GoNextDay()
    {
        if (NextDayPanel.gameObject.activeInHierarchy == false)
        {
            NextDayPanel.SetActive(true);
            ClosePanel.SetActive(true);
            ClosePanel.GetComponent<Button>().onClick.AddListener(GoNextDay);
        }
        else
        {
            NextDayPanel.SetActive(false);
            ClosePanel.SetActive(false);
            ClosePanel.GetComponent<Button>().onClick.RemoveListener(GoNextDay);
        }
    }
    public void ShowNameEditPanel()
    {
        if (NameEditPanel.gameObject.activeInHierarchy == false)
        {
            NameEditPanel.SetActive(true);
            ClosePanel.SetActive(true);
            ClosePanel.GetComponent<Button>().onClick.AddListener(ShowNameEditPanel);
        }
        else
        {
            NameEditPanel.SetActive(false);
            ClosePanel.SetActive(false);
            ClosePanel.GetComponent<Button>().onClick.RemoveListener(ShowNameEditPanel);
        }
    }

    public void ShowCNameEditPanel()
    {
        if (CNameEditPanel.gameObject.activeInHierarchy == false)
        {
            CNameEditPanel.SetActive(true);
            ClosePanel.SetActive(true);
            ClosePanel.GetComponent<Button>().onClick.AddListener(ShowCNameEditPanel);
        }
        else
        {
            CNameEditPanel.SetActive(false);
            ClosePanel.SetActive(false);
            ClosePanel.GetComponent<Button>().onClick.RemoveListener(ShowCNameEditPanel);
        }
    }

    public void OpenPhone(bool isopen)
    {
        if (PhoneCG.alpha == 1f)
            isopen = false;
        PhoneCG.alpha = isopen ? 1.0f : 0.0f;
        PhoneCG.blocksRaycasts = isopen;
        PhoneCG.interactable = isopen;
        Time.timeScale = isopen ? 0.0f : 1.0f;
    }

    public void ShowAlarmPanel()
    {
        if (AlarmPanel.gameObject.activeInHierarchy == false)
        {
            AlarmPanel.SetActive(true);
            ClosePanel.SetActive(true);
            ClosePanel.GetComponent<Button>().onClick.AddListener(ShowAlarmPanel);
        }
        else
        {
            AlarmPanel.SetActive(false);
            ClosePanel.SetActive(false);
            ClosePanel.GetComponent<Button>().onClick.RemoveListener(ShowAlarmPanel);
        }
    }

}
