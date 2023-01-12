using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;
public class SaveSlot : MonoBehaviour
{
    //public GameDataObject SaveData;
    public GameData SaveData;
    Text PlayerNameText;
    public GameObject NamePanel;
    public InputField playerNameInput;
    public int slotnum;
    public bool isThereFile = false;
    public Button DeleteButton;
    void Start()
    {
        //SaveData = Resources.Load<GameDataObject>($"SaveData/GameDataSlot{slotnum}");
        SaveData = DataManager.dataManager.Load(slotnum);
        PlayerNameText = transform.GetChild(2).GetComponent<Text>();
        PlayerNameText.text = SaveData.PlayerName;
        isThereFile = DataManager.dataManager.IsThereExist(slotnum);
        DeleteButton.interactable = isThereFile;
    }
    public void ClickedSlot()
    {
        if (!SaveData.IsStarted)
        {
            ShowNamePanel();
        }
        else
        {
            StartSceneManager.startSceneManager.GoGame(slotnum);
        }
    }
    public void ShowNamePanel()
    {
        NamePanel.SetActive(true);
        NamePanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(ChangePlayerName);
    }

    public void ChangePlayerName()
    {
        SaveData.PlayerName = playerNameInput.text;
        NamePanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveListener(ChangePlayerName);
        SaveData.IsStarted = true;
        Debug.Log(PlayerNameText.text);
        Debug.Log(SaveData.PlayerName);
        PlayerNameText.text = SaveData.PlayerName;
        DataManager.dataManager.SetDataPath(slotnum);
        DataManager.dataManager.Save(SaveData);
        StartSceneManager.startSceneManager.GoGame(slotnum);
   }

    //세이브 파일 리셋용
    public void ResetSaveData()
    {

        DataManager.dataManager.DeleteSaveData(slotnum);
        SaveData = DataManager.dataManager.Load(slotnum);
        PlayerNameText.text = SaveData.PlayerName;
        //AssetDatabase.DeleteAsset($"Assets/04.Scripts/Common/DataManager/GameDataSlot{slotnum}.asset");
        //AssetDatabase.CopyAsset("Assets/04.Scripts/Common/DataManager/GameDataSO.asset", $"Assets/04.Scripts/Common/DataManager/GameDataSlot{slotnum}.asset");

        //SaveData = (GameDataObject)AssetDatabase.LoadAssetAtPath($"Assets/04.Scripts/Common/DataManager/GameDataSlot{slotnum}.asset", typeof(GameDataObject));
        //PlayerNameText.text = SaveData.PlayerName;
    }
}
