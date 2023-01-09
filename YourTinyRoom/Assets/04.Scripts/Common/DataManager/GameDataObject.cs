using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using UnityEngine.Tilemaps;
//어튜리뷰트를 사용해서 자동 생성 
[CreateAssetMenu(fileName = "GameDataSO",
            menuName = "Create GameData", order = 1)]
//생성할 메뉴의 순서를 결정
public class GameDataObject : ScriptableObject
{
    public bool IsStarted=false;
    //게임 기본 정보
    public string PlayerName = "플레이어";
    public string CharacterName = "캐릭터";
    public float Gold = 1000f;
    public float Crystal = 10f;
    public int Level = 1;
    public float Exp = 0f;
    public float DayCount=0f;
    public float Like = 0f;
    public float Energy = 50f;
    public bool IsVisited = false;
    public bool isHome = false;
    public Vector3 CharacterPos= new Vector3(15f,-15f,0f);

    //인벤토리
    public List<ItemInInventory> ItemInInventories = new List<ItemInInventory>();

    //콜렉션
    public List<CollectItem> CollectItems = new List<CollectItem>();

    //퀘스트 리스트
    public List<QuestInList> questInLists = new List<QuestInList>();

    //캐릭터 커스텀
    public Color hairTintColor=Color.white;
    public Color hairDarkColor;
    public Color pupilTintColor = Color.white;
    public Color pupilDarkColor;
    public Color clothesTintColor = Color.white;
    public Color clothesDarkColor;

    public int activeHairIndex = 0;
    public int activeEyesIndex = 0;
    public int activeEyelashIndex = 0;
    public int activeClothIndex = 0;

    public string TailSkin = "";
    public string HairBackSkin = "";

    //사운드
    public float BGMVolume = 1f;
    public float SFXVolume = 1f;

    //맵
    public List<PlacedObject> PlacedObjectsInMap;

    //이벤트
    public List<DialogEvent> dialogEvents;

}