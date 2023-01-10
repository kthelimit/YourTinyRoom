using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager startSceneManager;
    public GameObject SaveSlotPanel;
    public GameObject ButtonPanel;
    public GameObject ClosePanel;
    public GameDataObject currentGameData;


    private void Awake()
    {
        if (startSceneManager == null)
            startSceneManager = this;
        else if (startSceneManager != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowSaveSlot()
    {
        if (SaveSlotPanel.gameObject.activeInHierarchy == false)
        {
            SaveSlotPanel.SetActive(true);
            ButtonPanel.SetActive(false);
            ClosePanel.SetActive(true);
            ClosePanel.GetComponent<Button>().onClick.AddListener(ShowSaveSlot);
        }
        else
        {
            SaveSlotPanel.SetActive(false);
            ButtonPanel.SetActive(true);
            ClosePanel.SetActive(false);
            ClosePanel.GetComponent<Button>().onClick.RemoveListener(ShowSaveSlot);
        }
    }

    public void GoGame(int num)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SceneLoader");
        currentGameData = (GameDataObject)AssetDatabase.LoadAssetAtPath($"Assets/04.Scripts/Common/DataManager/GameDataSlot{num}.asset", typeof(GameDataObject));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
