using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager startSceneManager;
    public GameObject SaveSlotPanel;
    public GameObject ButtonPanel;
    public GameObject ClosePanel;
    public GameObject NamePanel;
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
            if(NamePanel.gameObject.activeInHierarchy == true)
            {
                NamePanel.SetActive(false);
            }
            SaveSlotPanel.SetActive(false);
            ButtonPanel.SetActive(true);
            ClosePanel.SetActive(false);
            ClosePanel.GetComponent<Button>().onClick.RemoveListener(ShowSaveSlot);
        }
    }

    public void GoGame(int num)
    {
        SceneManager.LoadScene("SceneLoader");
        currentGameData = Resources.Load<GameDataObject>($"SaveData/GameDataSlot{num}");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
