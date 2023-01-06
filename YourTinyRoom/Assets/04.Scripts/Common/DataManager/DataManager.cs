using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DataInfo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Dictionary<int, Item> dicItem;
    private DataManager() { }

    public static DataManager GetInstance()
    {
        if(DataManager.instance==null)
        {
            DataManager.instance = new DataManager();
        }
        return DataManager.instance;
    }
    private string dataPath;

    public void Initialize()
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
    }

    public void Save(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        GameData data = new GameData();
        data.PlayerName = gameData.PlayerName;
        data.CharacterName = gameData.CharacterName;
        data.Gold = gameData.Gold;
        data.Crystal = gameData.Crystal;
        data.Level = gameData.Level;
        data.Exp = gameData.Exp;
        data.ItemInInventories = gameData.ItemInInventories;
        data.CollectItems = gameData.CollectItems;
        data.questInLists = gameData.questInLists;

        bf.Serialize(file, data);
        file.Close();
    }

    public GameData Load()
    {
        if(File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            Debug.Log("데이터가 없어서 새로 만들었어.");
            GameData data = new GameData();
            return data;
        }
    }

}
