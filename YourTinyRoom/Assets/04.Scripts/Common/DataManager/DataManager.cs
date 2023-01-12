using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DataInfo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager; //싱글턴
    public Dictionary<int, Item> dicItem;
    [SerializeField]
    private string dataPath;

    ConvertData convertData;

    private void Awake()
    {
        if (dataManager == null)
            dataManager = this;
        else if (dataManager != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        convertData = GetComponent<ConvertData>();
    }

    //저장할 데이터패스
    public void SetDataPath(int num)
    {
        dataPath = Application.persistentDataPath + $"/gameData{num}.dat";
    }


    public void Save(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        GameDataForSave data = convertData.ConvertDataToSaveData(gameData);

        bf.Serialize(file, data);
        file.Close();
    }

    public GameData Load(int num)
    {
        string LoaddataPath = Application.persistentDataPath + $"/gameData{num}.dat";
        if (File.Exists(LoaddataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(LoaddataPath, FileMode.Open);
            GameDataForSave SaveData = (GameDataForSave)bf.Deserialize(file);
            GameData data = convertData.ConvertDataToGameData(SaveData);
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

    public bool IsThereExist(int num)
    {
        string LoaddataPath = Application.persistentDataPath + $"/gameData{num}.dat";
        if (File.Exists(LoaddataPath))
        {
            return true;
        }
        return false;
    }

    //세이브 파일 리셋용
    public void DeleteSaveData(int num)
    {
        dataPath = Application.persistentDataPath + $"/gameData{num}.dat";
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }
    }

}
