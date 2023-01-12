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

    LevelTable levelTable;
    private int levelCount=1;
    public bool isAdded = false;

    private void Awake()
    {
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        LevelUpSFX = Resources.Load<AudioClip>("SFX/Complete_Level_01");
        levelTable = Resources.Load<LevelTable>("levelTable");
    }
 

    public void LoadLevel(int _level)
    {
        levelCount = _level;
        beforeLevel = _level;
        GameManager.gameManager.ChangeExpInterval(levelTable.leveltable[levelCount - 1].expLimit, levelTable.leveltable[levelCount].expLimit);
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
        if(exp >= levelTable.leveltable[levelCount].expLimit)
        {
            levelCount++;
            levelUpPanel.SetActive(true);
            SoundManager.soundManager.PlaySfx(this.transform.position, LevelUpSFX);
            levelText.text = beforeLevel.ToString() + '→' + levelCount.ToString();
            totalGold += levelTable.leveltable[levelCount-1].rewardGold;
            GoldText.text = totalGold.ToString();
            totalCrystal += levelTable.leveltable[levelCount-1].rewardCrystal;
            CrystalText.text = totalCrystal.ToString();
            Debug.Log($"{levelCount}: crystal {totalCrystal}, gold {totalGold}");
            GameManager.gameManager.ChangeExpInterval(levelTable.leveltable[levelCount - 1].expLimit, levelTable.leveltable[levelCount].expLimit);
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
