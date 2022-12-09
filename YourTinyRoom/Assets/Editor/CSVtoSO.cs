using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Linq ;

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
            //asset파일로 저장
            AssetDatabase.CreateAsset(item, $"Assets/Resources/Item/Item{item.ItemNumber.ToString("000")}_{item.ItemName}.asset");
                
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
                if (splitData[3] == itemlist[i].ItemName)
                {
                    quest.questItem = itemlist[i];
                    break;
                } 
            }
            //퀘스트에 필요한 아이템 수량
            quest.questItemQuantity = int.Parse(splitData[4]);

            //퀘스트 보상 아이템1
            for (int i = 0; i < itemlist.Length; i++)
            {
                if (splitData[5] == itemlist[i].ItemName)
                {
                    quest.RewardItem1 = itemlist[i];
                    break;
                }
            }
            //퀘스트 보상 아이템1 수량
            quest.RewardQuantity1 = float.Parse(splitData[6]);

            //퀘스트 보상 아이템2
            for (int i = 0; i < itemlist.Length; i++)
            {
                if (splitData[7] == itemlist[i].ItemName)
                {
                    quest.RewardItem2 = itemlist[i];
                    break;
                }
            }
            //퀘스트 보상 아이템2 수량
            quest.RewardQuantity2 = float.Parse(splitData[8]);
            //asset파일로 추출
            AssetDatabase.CreateAsset(quest, $"Assets/Resources/Quest/Quest{quest.questID}.asset");

        }
        AssetDatabase.SaveAssets();

    }
}
