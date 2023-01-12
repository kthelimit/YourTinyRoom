using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DataInfo;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager startSceneManager;
    public GameObject SaveSlotPanel;
    public CanvasGroup SaveSlotCG;
    public GameObject ButtonPanel;
    public GameObject ClosePanel;
    public GameObject NamePanel;
    //public GameDataObject currentGameData;
    public GameData currentGameData;

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
        if (SaveSlotCG.alpha == 1f)
        {
            if (NamePanel.gameObject.activeInHierarchy == true)
            {
                NamePanel.SetActive(false);
            }
            SaveSlotCG.alpha = 0.0f;
            SaveSlotCG.blocksRaycasts = false;
            SaveSlotCG.interactable = false;
            ButtonPanel.SetActive(true);
            ClosePanel.SetActive(false);
            ClosePanel.GetComponent<Button>().onClick.RemoveListener(ShowSaveSlot);
        }
        else
        {
            SaveSlotCG.alpha = 1.0f;
            SaveSlotCG.blocksRaycasts = true;
            SaveSlotCG.interactable = true;
            ButtonPanel.SetActive(false);
            ClosePanel.SetActive(true);
            ClosePanel.GetComponent<Button>().onClick.AddListener(ShowSaveSlot);
        }

        //if (SaveSlotPanel.gameObject.activeInHierarchy == false)
        //{
        //    SaveSlotPanel.SetActive(true);
        //    ButtonPanel.SetActive(false);
        //    ClosePanel.SetActive(true);
        //    ClosePanel.GetComponent<Button>().onClick.AddListener(ShowSaveSlot);
        //}
        //else
        //{
        //    if(NamePanel.gameObject.activeInHierarchy == true)
        //    {
        //        NamePanel.SetActive(false);
        //    }
        //    SaveSlotPanel.SetActive(false);
        //    ButtonPanel.SetActive(true);
        //    ClosePanel.SetActive(false);
        //    ClosePanel.GetComponent<Button>().onClick.RemoveListener(ShowSaveSlot);
        //}
    }


    public void GoGame(int num)
    {
        currentGameData = DataManager.dataManager.Load(num);
        DataManager.dataManager.SetDataPath(num);
        SceneManager.LoadScene("SceneLoader");
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
