using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DataInfo;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;//싱글턴
    public Camera mainCam;
    //자원관리
    private float gold =2000f;
    public Text goldText;
    private float crystal =10f;
    public Text crystalText;
    //날짜
    private float dayCount =0;
    public Text dayCountText;

    //경험치. 추후 레벨 테이블 구성 및 단계 구현하기
    private float curExp = 0f;
    private float minExp = 0f;
    private float maxExp = 30f;
    public Image expGauge;
    public Text levelText;
    LevelSystem levelSystem;

    //플레이어 이름
    public string playerName;
    public Text playerNameText;
    public InputField playerNameField;

    //캐릭터 이름
    public string characterName;
    public Text characterNameText;
    public Text characterNameText2;
    public InputField characterNameField;
    public Text characterNameText3;

    //먼지 생성
    private GameObject dustPrefab;
    private CharacterCtrl characterCtrl;
    public GameControl gameControl;
    public int DustRemoveCount=0;

    //데이터 매니저
    public DataManager dataManager;
    public Inventory Inventory;
    public Collections collections;
    public QuestManager questManager;
    public ColorChange colorChange;
    public GameDataObject gameData;
    public CharacterCustom characterCustom;
    public Transform FurnitureFolder;

    void Awake()
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        Inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        collections = GameObject.Find("Collection").GetComponent<Collections>();
        questManager = GetComponent<QuestManager>();
        FurnitureFolder = GameObject.Find("FurnitureFolder").transform;
        playerName = "플레이어";
        playerNameText.text = playerName;
        characterCtrl = GameObject.FindGameObjectWithTag("CHARACTER").transform.GetComponent<CharacterCtrl>();
        characterName = "캐릭터";
        characterNameText.text = characterName;
        characterNameText2.text = characterName + " 방문중!";
        levelSystem = transform.GetComponent<LevelSystem>();
        colorChange = characterCtrl.GetComponent<ColorChange>();
        dustPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/Dust.prefab", typeof(GameObject));
        characterCustom = characterCtrl.GetComponent<CharacterCustom>();
        gameData = GameObject.Find("SceneManager").GetComponent<StartSceneManager>().currentGameData;
    }
    public void LoadGameData()
    {
        //플레이어 이름
        playerName = gameData.PlayerName;
        playerNameText.text = playerName;

        //캐릭터 이름
        characterName = gameData.CharacterName;
        characterNameText.text = characterName;
        characterNameText3.text = characterName;
        characterNameText2.text = characterName + " 방문중!";

        //경험치, 골드, 크리스탈, 날짜, 호감도
        curExp = gameData.Exp;
        gold = gameData.Gold;
        crystal = gameData.Crystal;
        dayCount = gameData.DayCount;
        characterCtrl.LoadData(gameData.Like, gameData.Energy);

        goldText.text = gold.ToString("#,###");
        crystalText.text = crystal.ToString("#,###");
        dayCountText.text = dayCount.ToString();
        expGauge.fillAmount = curExp / maxExp;

        characterCtrl.isHome = gameData.isHome;
        characterCtrl.IsVisited = gameData.IsVisited;
        if(characterCtrl.IsVisited)
        {
            gameControl.OpenCharacterVisit(true);
            characterCtrl.InviteBtn.interactable = false;
            characterCtrl.phoneMessageButton.SetActive(false);
        }
        characterCtrl.tr.position = gameData.CharacterPos;

        //인벤토리
        Inventory.LoadInventory(gameData.ItemInInventories);
        //콜렉션
        collections.LoadCollections(gameData.CollectItems);
        //레벨
        levelSystem.LoadLevel(gameData.Level);
        //퀘스트리스트
        questManager.LoadQuestList(gameData.questInLists);

        //캐릭터 커스텀
        colorChange.LoadColorData(gameData.hairTintColor, gameData.hairDarkColor, gameData.pupilTintColor, gameData.pupilDarkColor, gameData.clothesTintColor, gameData.clothesDarkColor);
        characterCustom.LoadSkinData(gameData.activeHairIndex, gameData.activeEyesIndex, gameData.activeEyelashIndex, gameData.activeClothIndex, gameData.TailSkin, gameData.HairBackSkin);

        //사운드
        SoundManager.soundManager.LoadVolume(gameData.SFXVolume, gameData.BGMVolume);
        mainCam.GetComponent<AudioSource>().volume = gameData.BGMVolume;
        
        //맵데이터
        foreach(PlacedObject placedObj in gameData.PlacedObjectsInMap)
        {
           GameObject obj= Instantiate(placedObj.placedObject, FurnitureFolder);
           obj.transform.position = placedObj.pos;
            if(obj.tag=="FURNITURE")
            {
                Building _building = obj.transform.GetComponent<Building>();
                _building.UpdateSortingOrder();     
               
            }                
        }
        //이벤트 데이터
        if (gameData.dialogEvents.Count != 0)
        {
            for (int i = 0; i < gameData.dialogEvents.Count; i++)
            {
                DialogSystem.dialogSystem.dialogEvents[i].isFinished = gameData.dialogEvents[i].isFinished;
            }        
        }

    }

    public void SaveGameData()
    {
        gameData.PlayerName = playerName;
        gameData.CharacterName = characterName;
        gameData.Exp = curExp;
        gameData.Gold = gold;
        gameData.Crystal = crystal;
        gameData.DayCount = dayCount;
        gameData.ItemInInventories = Inventory.SaveInventory();
        gameData.CollectItems = collections.SaveCollections();
        gameData.Level = levelSystem.SaveLevel();
        gameData.questInLists = questManager.SaveQuestList();
        gameData.Like = characterCtrl.SaveData(1);
        gameData.Energy = characterCtrl.SaveData(2);

        gameData.CharacterPos = characterCtrl.tr.position;

        gameData.hairTintColor = colorChange.hairTintColor;
        gameData.hairDarkColor = colorChange.hairDarkColor;
        gameData.pupilTintColor = colorChange.pupilTintColor;
        gameData.pupilDarkColor = colorChange.pupilDarkColor;
        gameData.clothesTintColor = colorChange.clothesTintColor;
        gameData.clothesDarkColor = colorChange.clothesDarkColor;

        gameData.activeHairIndex = characterCustom.activeHairIndex;
        gameData.activeEyesIndex = characterCustom.activeEyesIndex;
        gameData.activeEyelashIndex = characterCustom.activeEyelashIndex;
        gameData.activeClothIndex = characterCustom.activeClothIndex;
        gameData.TailSkin = characterCustom.TailSkin;
        gameData.HairBackSkin = characterCustom.HairBackSkin;

        gameData.SFXVolume= SoundManager.soundManager.sfxVolume;
        gameData.BGMVolume = Camera.main.transform.GetComponent<AudioSource>().volume;

        //맵 내의 가구 리스트 저장
        Building[] furnitureList = FurnitureFolder.GetComponentsInChildren<Building>();
        gameData.PlacedObjectsInMap = null;
        gameData.PlacedObjectsInMap = new List<PlacedObject>();
        for(int i=0; i< furnitureList.Length;i++)
        {
            PlacedObject placedObject = new PlacedObject();
            placedObject.placedObject = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/05.Prefabs/Furniture/Item{furnitureList[i].GetComponent<ItemInfo>().item.ItemNumber.ToString("000")}.prefab", typeof(GameObject));
            placedObject.pos = furnitureList[i].transform.position;
            gameData.PlacedObjectsInMap.Add(placedObject);
        }
        //맵 내의 먼지 리스트 저장
        Dust[] DustList = FurnitureFolder.GetComponentsInChildren<Dust>();
        for(int i=0;i<DustList.Length;i++)
        {
            PlacedObject placedObject = new PlacedObject();
            placedObject.placedObject = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/Dust.prefab", typeof(GameObject));
            placedObject.pos = DustList[i].transform.position;
            Debug.Log(placedObject.pos);
            gameData.PlacedObjectsInMap.Add(placedObject);
        }

        gameData.dialogEvents = new List<DialogEvent>();
        for(int i=0; i<DialogSystem.dialogSystem.dialogEvents.Count;i++)
        {
            DialogEvent de = new DialogEvent();
            de.EventNumber = DialogSystem.dialogSystem.dialogEvents[i].EventNumber;
            de.isFinished = DialogSystem.dialogSystem.dialogEvents[i].isFinished;
            gameData.dialogEvents.Add(de);
        }

        gameData.isHome= characterCtrl.isHome;
        gameData.IsVisited = characterCtrl.IsVisited;

        Debug.Log("데이터 저장중");
        UnityEditor.EditorUtility.SetDirty(gameData);

    }

    void Start()
    {
        LoadGameData();

    }

    public bool IsBuyable(float price, int priceType)
    {
        if(priceType==1)
        {
            if (price <= crystal)
                return true;
            else
                return false;
        }
        else
        {
            if (price <= gold)
                return true;
            else
                return false;
        }
    }


    public void IncreaseGold(float amount)
    {
        gold += amount;
        goldText.text = gold.ToString("#,###");
    }
    public void DecreaseGold(float amount)
    {
        gold -= amount;
        goldText.text = gold.ToString("#,###");
    }


    public void IncreaseCrystal(float amount)
    {
        crystal += amount;
        crystalText.text = crystal.ToString("#,###");
    }
    public void DecreaseCrystal(float amount)
    {
        crystal -= amount;
        crystalText.text = crystal.ToString("#,###");
    }

    public void IncreaseExp(float amount)
    {
        curExp += amount;
        levelSystem.LevelUpCheck(curExp);
        StartCoroutine("ExpBarAnimation");
    }

    IEnumerator ExpBarAnimation()
    {        
        while (true)
        {
            expGauge.fillAmount = Mathf.Lerp(expGauge.fillAmount, (curExp - minExp) / (maxExp - minExp), 0.1f);
            yield return null;
            if (Mathf.Abs((curExp - minExp) / (maxExp - minExp) - expGauge.fillAmount) < 0.01f)
                break;
        }
        expGauge.fillAmount = (curExp - minExp) / (maxExp - minExp);
    }
    public void ChangeExpInterval(float MinExp, float MaxExp)
    {
        minExp = MinExp;
        maxExp = MaxExp;

    }

    public void UpdateLevelText(int _level)
    {
        levelText.text = "Lv " + _level.ToString();
    }

    public void ChangePlayerName()
    {        
        playerName= playerNameField.text;
        playerNameText.text = playerName;
        gameControl.ShowNameEditPanel();
    }

    public void ChangeCharacterName()
    {
        characterName = characterNameField.text;
        characterNameText.text = characterName;
        characterNameText3.text = characterName;
        characterNameText2.text = characterName+" 방문중!";
        gameControl.ShowCNameEditPanel();
    }

    public void GoNextDay()
    {
        dayCount++;
        dayCountText.text = dayCount.ToString();
        characterCtrl.StartAtHome();
        gameControl.GoNextDay();
        
        MakeDust();
    }

    //먼지가 랜덤한 위치에 하나 생기는 코드
    public void MakeDust()
    {
        float dustPosX = Random.Range(-5.3f, 5.3f);
        float dustPosY = Random.Range(-1.8f, 3.5f);
        if (Mathf.Abs(dustPosX) + Mathf.Abs(dustPosY) >= 5.3f)
        {
            if (dustPosY <= 0)
            {
                dustPosY = -5.3f + Mathf.Abs(dustPosX);
            }
            else
            {
                dustPosY = 5.3f - Mathf.Abs(dustPosX);
            }
        }
        Instantiate(dustPrefab, new Vector3(dustPosX, dustPosY, 0f), Quaternion.identity);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
