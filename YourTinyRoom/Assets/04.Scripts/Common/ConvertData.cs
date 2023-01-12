using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

public class ConvertData : MonoBehaviour
{
    public GameData ConvertDataToGameData(GameDataForSave SaveData)
    {
        GameData data = new GameData();

        data.IsStarted = SaveData.IsStarted;
        //게임 기본 정보
        data.PlayerName = SaveData.PlayerName;
        data.CharacterName = SaveData.CharacterName;
        data.Gold = SaveData.Gold;
        data.Crystal = SaveData.Crystal;
        data.Level = SaveData.Level;
        data.Exp = SaveData.Exp;
        data.DayCount = SaveData.DayCount;
        data.Like = SaveData.Like;
        data.Energy = SaveData.Energy;
        data.IsVisited = SaveData.IsVisited;
        data.isHome = SaveData.isHome;


        //사운드
        data.BGMVolume = SaveData.BGMVolume;
        data.SFXVolume = SaveData.SFXVolume;


        //이벤트
        data.dialogEvents = SaveData.dialogEvents;

        //인벤토리
        data.ItemInInventories= ConvertInventoryItemListForLoad(SaveData.ItemInInventories);


        //콜렉션
        data.CollectItems = ConvertCollectItemForLoad(SaveData.CollectItems);


        //캐릭터 커스텀
        data.hairTintColor=ArrayToColor(SaveData.hairTintColor);
        data.hairDarkColor = ArrayToColor(SaveData.hairDarkColor);
        data.pupilTintColor = ArrayToColor(SaveData.pupilTintColor);
        data.pupilDarkColor = ArrayToColor(SaveData.pupilDarkColor);
        data.clothesTintColor = ArrayToColor(SaveData.clothesTintColor);
        data.clothesDarkColor = ArrayToColor(SaveData.clothesDarkColor);

        data.activeHairIndex = SaveData.activeHairIndex;
        data.activeEyesIndex = SaveData.activeEyesIndex;
        data.activeEyelashIndex = SaveData.activeEyelashIndex;
        data.activeClothIndex = SaveData.activeClothIndex;

        data.TailSkin = SaveData.TailSkin;
        data.HairBackSkin = SaveData.HairBackSkin;

        //맵
        data.PlacedObjectsInMap = ConvertPlacedObjectForLoad(SaveData.PlacedObjectsInMap);

        //퀘스트 리스트
        data.questInLists = ConvertQuestInListForLoad(SaveData.questInLists);



        return data;
    }

    public GameDataForSave ConvertDataToSaveData(GameData data)
    {
        GameDataForSave SaveData = new GameDataForSave();
        SaveData.IsStarted = data.IsStarted;
        //게임 기본 정보
        SaveData.PlayerName = data.PlayerName;
        SaveData.CharacterName = data.CharacterName;
        SaveData.Gold = data.Gold;
        SaveData.Crystal = data.Crystal;
        SaveData.Level = data.Level;
        SaveData.Exp = data.Exp;
        SaveData.DayCount = data.DayCount;
        SaveData.Like = data.Like;
        SaveData.Energy = data.Energy;
        SaveData.IsVisited = data.IsVisited;
        SaveData.isHome = data.isHome;

        //사운드
        SaveData.BGMVolume = data.BGMVolume;
        SaveData.SFXVolume = data.SFXVolume;

        //이벤트
        SaveData.dialogEvents = data.dialogEvents;

        //인벤토리
        SaveData.ItemInInventories=ConvertInventoryItemListForSave(data.ItemInInventories);

        //콜렉션
        SaveData.CollectItems = ConvertCollectItemForSave(data.CollectItems);

        //캐릭터 커스텀
        SaveData.hairTintColor=ColorToArray(data.hairTintColor);
        SaveData.hairDarkColor = ColorToArray(data.hairDarkColor);
        SaveData.pupilTintColor = ColorToArray(data.pupilTintColor);
        SaveData.pupilDarkColor = ColorToArray(data.pupilDarkColor);
        SaveData.clothesTintColor = ColorToArray(data.clothesTintColor);
        SaveData.clothesDarkColor = ColorToArray(data.clothesDarkColor);

        SaveData.activeHairIndex = data.activeHairIndex;
        SaveData.activeEyesIndex = data.activeEyesIndex;
        SaveData.activeEyelashIndex = data.activeEyelashIndex;
        SaveData.activeClothIndex = data.activeClothIndex;

        SaveData.TailSkin = data.TailSkin;
        SaveData.HairBackSkin = data.HairBackSkin;

        //맵
        SaveData.PlacedObjectsInMap = ConvertPlacedObjectForSave(data.PlacedObjectsInMap);

        //퀘스트 리스트
        SaveData.questInLists = ConvertQuestInListForSave(data.questInLists);



        return SaveData;
    }

    public float[] ColorToArray(Color color)
    {
        float[] array = new float[4];
        array[0] = color.r;
        array[1] = color.g;
        array[2] = color.b;
        array[3] = color.a;
        return array;
    }

    public Color ArrayToColor(float[] array)
    {
        return new Color(array[0], array[1], array[2], array[3]);
    }


    public float[] Vector3ToArray(Vector3 pos)
    {
        float[] array = new float[3];
        array[0] = pos.x;
        array[1] = pos.y;
        array[2] = pos.z;
        return array;
    }

    public Vector3 ArrayToVector3(float[] array)
    {
        return new Vector3(array[0], array[1], array[2]);
    }

    public List<ItemsInInventoryForSave> ConvertInventoryItemListForSave(List<ItemInInventory> ListOrigin)
    {
        List<ItemsInInventoryForSave> ListConvert = new List<ItemsInInventoryForSave>();
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (ItemInInventory objOrigin in ListOrigin)
        {
            ItemsInInventoryForSave objConvert = new ItemsInInventoryForSave();
            objConvert.Item = $"Item{objOrigin.item.ItemNumber.ToString("000")}_{objOrigin.item.ItemName}";
            objConvert.itemCount = objOrigin.itemCount;
            ListConvert.Add(objConvert);
        }
        return ListConvert;
    }

    public List<ItemInInventory> ConvertInventoryItemListForLoad(List<ItemsInInventoryForSave> ListOrigin)
    {
        List<ItemInInventory> ListConvert = new List<ItemInInventory>();
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (ItemsInInventoryForSave objOrigin in ListOrigin)
        {
            ItemInInventory objConvert = new ItemInInventory();
            objConvert.item = Resources.Load<Item>($"Item/{objOrigin.Item}");
            objConvert.itemCount = objOrigin.itemCount;
            ListConvert.Add(objConvert);
        }
        return ListConvert;
    }

    public List<QuestInListForSave> ConvertQuestInListForSave(List<QuestInList> ListOrigin)
    {
        List<QuestInListForSave> ListConvert = new List<QuestInListForSave>();
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (QuestInList objOrigin in ListOrigin)
        {
            QuestInListForSave objConvert = new QuestInListForSave();
            objConvert.questID= objOrigin.quest.questID;
            objConvert.isAlarmed= objOrigin.isAlarmed;
            objConvert.isCompleted= objOrigin.isCompleted;
            objConvert.isTakeOut= objOrigin.isTakeOut;
            ListConvert.Add(objConvert);
        }

        return ListConvert;
    }

    public List<QuestInList> ConvertQuestInListForLoad(List<QuestInListForSave> ListOrigin)
    {
        List<QuestInList> ListConvert = new List<QuestInList>();
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (QuestInListForSave objOrigin in ListOrigin)
        {
            QuestInList objConvert = new QuestInList();
            objConvert.quest = Resources.Load<Quest>($"Quest/Quest{objOrigin.questID}");
            objConvert.isAlarmed = objOrigin.isAlarmed;
            objConvert.isCompleted = objOrigin.isCompleted;
            objConvert.isTakeOut = objOrigin.isTakeOut;
            ListConvert.Add(objConvert);
        }

        return ListConvert;
    }

    public List<CollectItemForSave> ConvertCollectItemForSave(List<CollectItem> ListOrigin)
    {
        List<CollectItemForSave> ListConvert = new List<CollectItemForSave>();
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (CollectItem objOrigin in ListOrigin)
        {
            CollectItemForSave objConvert = new CollectItemForSave();
            objConvert.itemName = objOrigin.item.ItemName;
            objConvert.ItemNumber = objOrigin.ItemNumber;
            objConvert.isCollected = objOrigin.isCollected;
            ListConvert.Add(objConvert);
        }    

        return ListConvert;
    }

    public List<CollectItem> ConvertCollectItemForLoad(List<CollectItemForSave> ListOrigin)
    {
        List<CollectItem> ListConvert = new List<CollectItem>();
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (CollectItemForSave objOrigin in ListOrigin)
        {
            CollectItem objConvert = new CollectItem();
            objConvert.item= Resources.Load<Item>($"Item/Item{objOrigin.ItemNumber.ToString("000")}_{objOrigin.itemName}");
            objConvert.ItemNumber = objOrigin.ItemNumber;
            objConvert.isCollected = objOrigin.isCollected;

            ListConvert.Add(objConvert);
        }

        return ListConvert;
    }

    public List<PlacedObjectForSave> ConvertPlacedObjectForSave(List<PlacedObject> ListOrigin)
    {
        List<PlacedObjectForSave> ListConvert = new List<PlacedObjectForSave>();
        Debug.Log(ListOrigin.Count);
        Debug.Log(ListConvert.Count);
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (PlacedObject objOrigin in ListOrigin)
        {
            PlacedObjectForSave objConvert = new PlacedObjectForSave();
            if (objOrigin.placedObject.name == "Dust")
            {
                objConvert.placedObjectPath = $"Prefabs/{objOrigin.placedObject.name}";
            }
            else if(objOrigin.placedObject.name == "strawberry")
            {
                objConvert.placedObjectPath = $"Prefabs/{objOrigin.placedObject.name}";
            }
            else
            {
                objConvert.placedObjectPath = $"Prefabs/Furniture/{objOrigin.placedObject.name}";
            }            
            objConvert.pos = Vector3ToArray(objOrigin.pos);
            ListConvert.Add(objConvert);
        }
        return ListConvert;
    }

    public List<PlacedObject> ConvertPlacedObjectForLoad(List<PlacedObjectForSave> ListOrigin)
    {
        List<PlacedObject> ListConvert = new List<PlacedObject>();
        if (ListOrigin.Count == 0) return ListConvert;
        foreach (PlacedObjectForSave objOrigin in ListOrigin)
        {
            PlacedObject objConvert = new PlacedObject();
            objConvert.placedObject = Resources.Load<GameObject>(objOrigin.placedObjectPath);
            objConvert.pos = ArrayToVector3(objOrigin.pos);
            ListConvert.Add(objConvert);
        }

        return ListConvert;
    }

}
