using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
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
    private void Awake()
    {
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

    public void LevelUpCheck(float exp)
    {
        while(exp>=levelTable[levelCount].expLimit)
        {
            levelCount++;
            GameManager.gameManager.ChangeExpInterval(levelTable[levelCount-1].expLimit, levelTable[levelCount].expLimit);
            GameManager.gameManager.IncreaseExp(0);
            GameManager.gameManager.UpdateLevelText(levelCount);           
            GameManager.gameManager.IncreaseGold(levelTable[levelCount].rewardGold);
            GameManager.gameManager.IncreaseCrystal(levelTable[levelCount].rewardCrystal);
        }
    }

}
