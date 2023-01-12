using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public bool IsStarted = false;
        //게임 기본 정보
        public string PlayerName = "플레이어";
        public string CharacterName = "캐릭터";
        public float Gold = 1000f;
        public float Crystal = 10f;
        public int Level = 1;
        public float Exp = 0f;
        public float DayCount = 0f;
        public float Like = 0f;
        public float Energy = 50f;
        public bool IsVisited = false;
        public bool isHome = true;

        //인벤토리
        public List<ItemInInventory> ItemInInventories = new List<ItemInInventory>();

        //콜렉션
        public List<CollectItem> CollectItems = new List<CollectItem>();

        //퀘스트 리스트
        public List<QuestInList> questInLists = new List<QuestInList>();

        //캐릭터 커스텀
        public Color hairTintColor = Color.white;
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
        public List<PlacedObject> PlacedObjectsInMap = new List<PlacedObject>();

        //이벤트
        public List<DialogEvent> dialogEvents= new List<DialogEvent>();
    }
    [System.Serializable]
    public class ItemInInventory
    {
        public Item item;
        public int itemCount;
    }
    [System.Serializable]
    public class CollectItem
    {
        public Item item;
        public int ItemNumber;
        public bool isCollected;
    }
    [System.Serializable]
    public class QuestInList
    {
        public Quest quest;
        public bool isCompleted;
        public bool isTakeOut;
        public bool isAlarmed;
    }
    [System.Serializable]
    public class PlacedObject
    {
        public GameObject placedObject;
        public Vector3 pos;           
    }
    [System.Serializable]
    public class DialogEvent
    {
        public int EventNumber;
        public bool isFinished = false;

    }
    [System.Serializable]
    public class Reward
    {
        public Item item;
        public float quantity;
    }


    //데이터 저장용 새 클래스
    [System.Serializable]
    public class GameDataForSave
    {
        public bool IsStarted;
        //게임 기본 정보
        public string PlayerName;
        public string CharacterName;
        public float Gold;
        public float Crystal;
        public int Level;
        public float Exp;
        public float DayCount;
        public float Like;
        public float Energy;
        public bool IsVisited;
        public bool isHome;


        //인벤토리
        public List<ItemsInInventoryForSave> ItemInInventories = new List<ItemsInInventoryForSave>();

        //콜렉션
        public List<CollectItemForSave> CollectItems = new List<CollectItemForSave>();

        //퀘스트 리스트
        public List<QuestInListForSave> questInLists = new List<QuestInListForSave>();

        //캐릭터 커스텀
        public float[] hairTintColor = new float[4];
        public float[] hairDarkColor = new float[4];
        public float[] pupilTintColor = new float[4];
        public float[] pupilDarkColor = new float[4];
        public float[] clothesTintColor = new float[4];
        public float[] clothesDarkColor = new float[4];

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
        public List<PlacedObjectForSave> PlacedObjectsInMap = new List<PlacedObjectForSave>();

        //이벤트
        public List<DialogEvent> dialogEvents = new List<DialogEvent>();
    }

    [System.Serializable]
    public class ItemsInInventoryForSave
    {
        public string Item;
        public int itemCount;
    }

    [System.Serializable]
    public class QuestInListForSave
    {
        public int questID;
        public bool isCompleted;
        public bool isTakeOut;
        public bool isAlarmed;
    }

    [System.Serializable]
    public class CollectItemForSave
    {
        public string itemName;
        public int ItemNumber;
        public bool isCollected;
    }

    [System.Serializable]
    public class PlacedObjectForSave
    {
        public string placedObjectPath;
        public float[] pos = new float[3];
    }


}