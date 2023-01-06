using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
//어튜리뷰트를 사용해서 자동 생성 
[CreateAssetMenu(fileName = "GameDataSO",
            menuName = "Create GameData", order = 1)]
//생성할 메뉴의 순서를 결정
public class GameDataObject : ScriptableObject
{
    //게임 기본 정보
    public string PlayerName = "플레이어";
    public string CharacterName = "캐릭터";
    public float Gold = 1000f;
    public float Crystal = 10f;
    public int Level = 1;
    public float Exp = 0f;

    //인벤토리
    public List<ItemInInventory> ItemInInventories = new List<ItemInInventory>();

    //콜렉션
    public List<CollectItem> CollectItems = new List<CollectItem>();

    //퀘스트 리스트
    public List<QuestInList> questInLists = new List<QuestInList>();

    //캐릭터 커스텀
    public Color hairTintColor;
    public Color hairDarkColor;
    public Color pupilTintColor;
    public Color pupilDarkColor;
    public Color clothesTintColor;
    public Color clothesDarkColor;

    public int activeHairIndex = 0;
    public int activeEyesIndex = 0;
    public int activeEyelashIndex = 0;
    public int activeClothIndex = 0;

    public string TailSkin = "";
    public string HairBackSkin = "";

}