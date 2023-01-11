﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public GameDataObject SaveData;
    Text PlayerNameText;
    public GameObject NamePanel;
    public InputField playerNameInput;
    public int slotnum;
    void Awake()
    {
        SaveData = Resources.Load<GameDataObject>($"SaveData/GameDataSlot{slotnum}");
        PlayerNameText = transform.GetChild(2).GetComponent<Text>();
        PlayerNameText.text = SaveData.PlayerName;

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
        PlayerNameText.text = SaveData.PlayerName;
        StartSceneManager.startSceneManager.GoGame(slotnum);
   }


    public void ResetSaveData()
    {        
        //AssetDatabase.DeleteAsset($"Assets/04.Scripts/Common/DataManager/GameDataSlot{slotnum}.asset");
        //AssetDatabase.CopyAsset("Assets/04.Scripts/Common/DataManager/GameDataSO.asset", $"Assets/04.Scripts/Common/DataManager/GameDataSlot{slotnum}.asset");

        //SaveData = (GameDataObject)AssetDatabase.LoadAssetAtPath($"Assets/04.Scripts/Common/DataManager/GameDataSlot{slotnum}.asset", typeof(GameDataObject));
        //PlayerNameText.text = SaveData.PlayerName;
    }
}
