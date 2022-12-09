using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Linq ;

public class CSVtoSO
{
    private static string ItemCSVPath = "/Editor/CSVs/ItemCSV.csv";
    [MenuItem("Utilities/Generate Item")]
    public static void GenreateItems()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath+ ItemCSVPath);
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemSprites");
        int idx = 0;
        Debug.Log(sprites[1].name);
        foreach(string s in allLines)
        {
            string[] splitData = s.Split(',');
  
            Item item = ScriptableObject.CreateInstance<Item>();
            item.ItemNumber = int.Parse(splitData[0]);
            item.ItemName = splitData[1];
            item.itemType = (Item.ItemType)Enum.Parse(typeof(Item.ItemType),splitData[2]);
            item.ItemDesc = splitData[3];
            item.itemPriceType = (Item.ItemPriceType)Enum.Parse(typeof(Item.ItemPriceType), splitData[4]);
            item.ItemPrice = float.Parse(splitData[5]);
            //이미지 연결을 위해 이미지 이름도 무조건 번호를 맞춰서 넣고 중간에 쓸데없는 걸 넣지 말것.
            item.itemImage = sprites[idx++];           
            
            AssetDatabase.CreateAsset(item, $"Assets/Resources/Item/Item{item.ItemNumber}{item.ItemName}.asset");
                
        }
        AssetDatabase.SaveAssets();
    }

    private static string QuestCSVPath = "/Editor/CSVs/QuestCSV.csv";
    [MenuItem("Utilities/Generate Quest")]
    public static void GenreateQuests()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + QuestCSVPath);
        Item[] itemlist = Resources.LoadAll<Item>("Item");
        Debug.Log(itemlist[0].name);
        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');

            QuestData quest = ScriptableObject.CreateInstance<QuestData>();
            quest.questID = int.Parse(splitData[0]);
            quest.questName = splitData[1];
            quest.questDesc = splitData[2];
            for(int i=0;i<itemlist.Length;i++)
            {
                if (splitData[3] == itemlist[i].ItemName)
                {
                    quest.questItem = itemlist[i];
                    break;
                } 
            }
            quest.questItemQuantity = int.Parse(splitData[4]);
            for (int i = 0; i < itemlist.Length; i++)
            {
                if (splitData[5] == itemlist[i].ItemName)
                {
                    quest.RewardItem1 = itemlist[i];
                    break;
                }
            }
            quest.RewardQuantity1 = float.Parse(splitData[6]);
            for (int i = 0; i < itemlist.Length; i++)
            {
                if (splitData[7] == itemlist[i].ItemName)
                {
                    quest.RewardItem2 = itemlist[i];
                    break;
                }
            }
            quest.RewardQuantity2 = float.Parse(splitData[8]);
            AssetDatabase.CreateAsset(quest, $"Assets/Resources/Quest/Quest{quest.questID}.asset");

        }
        AssetDatabase.SaveAssets();

    }
}
