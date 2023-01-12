using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq ;
using DataInfo;
public class CSVtoSO
{
    //ItemCSV파일 저장된 경로
    private static string ItemCSVPath = "/Editor/CSVs/ItemCSV.csv";
    //위의 메뉴에 추가
    [MenuItem("Utilities/Generate Item")]
    public static void GenreateItems()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath+ ItemCSVPath);
        //이름을 확인하고 스프라이트 할당할 거라 미리 리스트로 불러온다.
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemSprites");
        foreach(string s in allLines)
        {
            string[] splitData = s.Split(',');
  
            Item item = ScriptableObject.CreateInstance<Item>();
            //아이템 번호
            item.ItemNumber = int.Parse(splitData[0]);
            //아이템 이름
            item.ItemName = splitData[1];
            //아이템 타입
            item.itemType = (Item.ItemType)Enum.Parse(typeof(Item.ItemType),splitData[2]);
            //아이템 설명
            item.ItemDesc = splitData[3];
            //아이템 가격을 지불할 자원
            item.itemPriceType = (Item.ItemPriceType)Enum.Parse(typeof(Item.ItemPriceType), splitData[4]);
            //아이템 가격
            item.ItemPrice = float.Parse(splitData[5]);
            //아이템 스프라이트
            string str = "Item"+item.ItemNumber.ToString("000");
            for (int i = 0; i < sprites.Length; i++)
            {
                if(sprites[i].name== str)
                {
                    item.itemImage = sprites[i];
                    break;
                }
            };
            if (item.itemType == Item.ItemType.FURNITURE)
            {
                Furniture furniture = ScriptableObject.CreateInstance<Furniture>();
                furniture.ItemNumber = int.Parse(splitData[0]);
                //아이템 이름
                furniture.ItemName = splitData[1];
                //아이템 타입
                furniture.itemType = (Item.ItemType)Enum.Parse(typeof(Item.ItemType), splitData[2]);
                //아이템 설명
                furniture.ItemDesc = splitData[3];
                //아이템 가격을 지불할 자원
                furniture.itemPriceType = (Item.ItemPriceType)Enum.Parse(typeof(Item.ItemPriceType), splitData[4]);
                //아이템 가격
                furniture.ItemPrice = float.Parse(splitData[5]);
                //아이템 스프라이트
                str = "Item" + item.ItemNumber.ToString("000");
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (sprites[i].name == str)
                    {
                        furniture.itemImage = sprites[i];
                        break;
                    }
                };
                furniture.area = new BoundsInt(0,0,0,int.Parse(splitData[6]), int.Parse(splitData[7]), int.Parse(splitData[8]));
                AssetDatabase.CreateAsset(furniture, $"Assets/Resources/Item/Item{item.ItemNumber.ToString("000")}_{item.ItemName}.asset");

            }
            else if (item.itemType == Item.ItemType.GIFT)
            {
                GIFT gift = ScriptableObject.CreateInstance<GIFT>();
                gift.ItemNumber = int.Parse(splitData[0]);
                //아이템 이름
                gift.ItemName = splitData[1];
                //아이템 타입
                gift.itemType = (Item.ItemType)Enum.Parse(typeof(Item.ItemType), splitData[2]);
                //아이템 설명
                gift.ItemDesc = splitData[3];
                //아이템 가격을 지불할 자원
                gift.itemPriceType = (Item.ItemPriceType)Enum.Parse(typeof(Item.ItemPriceType), splitData[4]);
                //아이템 가격
                gift.ItemPrice = float.Parse(splitData[5]);
                //아이템 스프라이트
                str = "Item" + item.ItemNumber.ToString("000");
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (sprites[i].name == str)
                    {
                        gift.itemImage = sprites[i];
                        break;
                    }
                };
                gift.itemEffectType= (GIFT.ItemEffectType)Enum.Parse(typeof(GIFT.ItemEffectType), splitData[6]);
                gift.ItemEffectValue= float.Parse(splitData[7]);

                AssetDatabase.CreateAsset(gift, $"Assets/Resources/Item/Item{item.ItemNumber.ToString("000")}_{item.ItemName}.asset");

            }
            else
            {//asset파일로 저장
                AssetDatabase.CreateAsset(item, $"Assets/Resources/Item/Item{item.ItemNumber.ToString("000")}_{item.ItemName}.asset");
            }
            

        }
        AssetDatabase.SaveAssets();
    }

    //QuestCSV파일 저장된 경로
    private static string QuestCSVPath = "/Editor/CSVs/QuestCSV.csv";
    //위의 메뉴에 추가 
    [MenuItem("Utilities/Generate Quest")]
    public static void GenreateQuests()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + QuestCSVPath);
        //아이템 비교해서 넣을 것이므로 일단 아이템리스트로 불러온다.
        Item[] itemlist = Resources.LoadAll<Item>("Item");
        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            Quest quest = ScriptableObject.CreateInstance<Quest>();
            //퀘스트 번호
            quest.questID = int.Parse(splitData[0]);
            //퀘스트 제목
            quest.questName = splitData[1];
            //퀘스트 설명
            quest.questDesc = splitData[2];

            //퀘스트에 필요한 아이템
            for(int i=0;i<itemlist.Length;i++)
            {
                if (int.Parse(splitData[3]) == itemlist[i].ItemNumber)
                {
                    quest.questItem = itemlist[i];
                    break;
                } 
            }
            //퀘스트에 필요한 아이템 수량
            quest.questItemQuantity = int.Parse(splitData[4]);

            //퀘스트 보상 아이템 1
            quest.RewardList = new System.Collections.Generic.List<Reward>();
            for (int i = 0; i < itemlist.Length; i++)
            {
                if (splitData[5] == itemlist[i].ItemName)
                {
                    Reward reward = new Reward();
                    reward.item = itemlist[i];
                    reward.quantity = float.Parse(splitData[6]);
                    quest.RewardList.Add(reward);
                    break;
                }
            }

            //퀘스트 보상 아이템2
            for (int i = 0; i < itemlist.Length; i++)
            {
                if (splitData[7] == itemlist[i].ItemName)
                {
                    Reward reward = new Reward();
                    reward.item = itemlist[i];
                    reward.quantity = float.Parse(splitData[8]);
                    quest.RewardList.Add(reward);
                    break;
                }
            }
            //asset파일로 추출
            AssetDatabase.CreateAsset(quest, $"Assets/Resources/Quest/Quest{quest.questID}.asset");

        }
        AssetDatabase.SaveAssets();

    }

    //QuestCSV파일 저장된 경로
    private static string LevelCSVPath = "/Editor/CSVs/LevelCSV.csv";

    //위의 메뉴에 추가 
    [MenuItem("Utilities/Generate LevelTable")]
    public static void GenerateLevelTable()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + LevelCSVPath);
        LevelTable levels = new LevelTable();
        levels.leveltable = new List<Level>();
        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');
            Level _level = new Level();
            _level.level = int.Parse(splitData[0]);
            _level.expInterval = float.Parse(splitData[1]);
            _level.expLimit = float.Parse(splitData[2]);
            _level.rewardGold = float.Parse(splitData[3]);
            _level.rewardCrystal = float.Parse(splitData[4]);
            levels.leveltable.Add(_level);
        }

        AssetDatabase.CreateAsset(levels, $"Assets/Resources/levelTable.asset");
    }
}
