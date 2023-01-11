using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public GameObject levelUpPanel;
    public Text levelText;
    public Text GoldText;
    public Text CrystalText;
    private int beforeLevel = 1;
    private float totalGold = 0;
    private float totalCrystal = 0;
    AudioClip LevelUpSFX;
    public Shop shop;
    GameControl gameControl;
    private struct Level
    {
        public int level;
        public float expInterval;
        public float expLimit;
        public float rewardGold;
        public float rewardCrystal;
    };
    List<Level> levelTable;
    private static string LevelCSVPath = "/Editor/CSVs/LevelCSV.csv";
    private int levelCount=1;
    public bool isAdded = false;

    private void Awake()
    {
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        LevelUpSFX = Resources.Load<AudioClip>("SFX/Complete_Level_01");
        LevelDataLoad();
    }
    private void LevelDataLoad()
    {
        levelTable = new List<Level>();
        string[] allLines = File.ReadAllLines(Application.dataPath + LevelCSVPath);
        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');
            Level _level = new Level();
            _level.level = int.Parse(splitData[0]);
            _level.expInterval = float.Parse(splitData[1]);
            _level.expLimit = float.Parse(splitData[2]);
            _level.rewardGold = float.Parse(splitData[3]);
            _level.rewardCrystal = float.Parse(splitData[4]);
            levelTable.Add(_level);
        }
    }

    public void LoadLevel(int _level)
    {
        levelCount = _level;
        GameManager.gameManager.ChangeExpInterval(levelTable[levelCount - 1].expLimit, levelTable[levelCount].expLimit);
        GameManager.gameManager.UpdateLevelText(levelCount);
        if(levelCount>=5)
        {
            shop.AddItemList(shop.itemListSecond);
            isAdded = true;
            shop.AssignSlot();
        }
    }
    public int SaveLevel()
    {
        return levelCount;
    }

    public void LevelUpCheck(float exp)
    {
        if(exp >= levelTable[levelCount].expLimit)
        {
            levelCount++;
            levelUpPanel.SetActive(true);
            SoundManager.soundManager.PlaySfx(this.transform.position, LevelUpSFX);
            levelText.text = beforeLevel.ToString() + '→' + levelCount.ToString();
            totalGold += levelTable[levelCount-1].rewardGold;
            GoldText.text = totalGold.ToString();
            totalCrystal += levelTable[levelCount-1].rewardCrystal;
            CrystalText.text = totalCrystal.ToString();
            Debug.Log($"{levelCount}: crystal {totalCrystal}, gold {totalGold}");
            GameManager.gameManager.ChangeExpInterval(levelTable[levelCount - 1].expLimit, levelTable[levelCount].expLimit);
            GameManager.gameManager.IncreaseExp(0);
        }      

        GameManager.gameManager.UpdateLevelText(levelCount);
        GameManager.gameManager.IncreaseGold(totalGold);
        GameManager.gameManager.IncreaseCrystal(totalCrystal);

        //레벨이 5 이상이 되면 상점에 리스트가 추가된다.
        if (levelCount >= 5 && !isAdded)
        {            
            shop.AddItemList(shop.itemListSecond);
            isAdded = true;
            shop.AssignSlot();
            gameControl.OpenDialog(true);
            DialogSystem.dialogSystem.Talk(300);
        }

        beforeLevel = levelCount;
        totalGold = 0f;
        totalCrystal = 0f;

    }

    public void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
    }
}
