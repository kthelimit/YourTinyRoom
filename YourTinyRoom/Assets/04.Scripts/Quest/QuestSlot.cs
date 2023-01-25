using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DataInfo;
public class QuestSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Quest questData;
    [SerializeField]
    private Button rewardButton;
    private Transform scrollContents;
    public bool isCompleted = false;
    public bool isTakeOut = false;
    public GameObject finishImg;
    public Text questTitle;
    public Text questDesc;
    Inventory inventory;
    public GameObject QuestAlarmPrefab;
    public GameObject rewardPrefab;
    public Text QuestAlarmPanelText;
    public bool isAlarmed = false;
    private Transform canvasUI;
    public int questItemCount;
    Transform rewardPanel;
    AudioClip QuestCompleteSFX;
    GameControl gameControl;

    GameObject RewardSlot;
    public Transform RewardShowPanelReward;

    private void Awake()
    {
        QuestCompleteSFX = Resources.Load<AudioClip>("SFX/Task_Complete_02");
        rewardPanel = transform.GetChild(3);
        QuestAlarmPrefab = Resources.Load<GameObject>("Prefabs/QuestAlarm");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/QuestAlarm.prefab", typeof(GameObject));
        QuestAlarmPanelText = QuestAlarmPrefab.transform.GetChild(0).GetComponent<Text>();
        rewardPrefab = Resources.Load<GameObject>("Prefabs/RewardItem");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/RewardItem.prefab", typeof(GameObject));
        canvasUI = GameObject.Find("Canvas-UI").transform;
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        scrollContents = this.transform.parent;
        rewardButton = transform.GetChild(0).GetComponent<Button>();
        rewardButton.interactable = false;
        RewardShowPanelReward = gameControl.RewardShowPanel.transform.GetChild(1).GetChild(0);
        RewardSlot = Resources.Load<GameObject>("Prefabs/RewardSlot");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/RewardSlot.prefab", typeof(GameObject));
    }
    public void SetQuest(QuestInList _quest)
    {
        questData = _quest.quest;
        questTitle.text = questData.questName;
        questDesc.text = questData.questDesc;
        isTakeOut = _quest.isTakeOut;
        isCompleted = _quest.isCompleted;
        isAlarmed = _quest.isAlarmed;
        SetReward();
        if(isTakeOut)
        {
            ChangeFinished();
        }
        else
        {
            this.transform.SetAsFirstSibling();
        }
        StartCoroutine("UpdateCheck");
    }

    public void SetReward()
    {
        foreach(Reward reward in questData.RewardList)
        {
            GameObject obj = Instantiate(rewardPrefab, rewardPanel);
            
            obj.GetComponentInChildren<Image>().sprite = reward.item.itemImage;
            obj.GetComponentInChildren<Text>().text= $"{reward.quantity}";
        }
    }

    IEnumerator UpdateCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CheckComplete();
            if (isCompleted && !isTakeOut)
            {
                QuestComplete();
            }
            else if(isCompleted&&isTakeOut)
            {
                break;
            }
        }
    }


    //퀘스트 달성 조건 만족
    public void QuestComplete()
    {
        if (isAlarmed) return;
        rewardButton.interactable = true;
        this.transform.SetAsFirstSibling();
        QuestManager.questManager.CheckIsThereReward();
        QuestManager.AllTakeOut += this.TakeOut;
        QuestAlarmPanelText.text = "퀘스트 [" + questData.questName + "] 달성! 보상을 수령해주세요.";
        StartCoroutine("ShowQuestAlarmPanel");
    }
    IEnumerator ShowQuestAlarmPanel()
    {
        SoundManager.soundManager.PlaySfx(this.transform.position, QuestCompleteSFX);
        GameObject questAlarm = Instantiate(QuestAlarmPrefab, canvasUI);
        questAlarm.GetComponent<Button>().onClick.AddListener(()=>gameControl.OpenQuestList(true));
        questAlarm.GetComponent<Animator>().SetTrigger("Down");
        isAlarmed = true;
        yield return new WaitForSeconds(3f);
        questAlarm.GetComponent<Animator>().SetTrigger("Up");
        Destroy(questAlarm, 1f);
    }

    //퀘스트 보상 수령
    public void TakeOut()
    {
        isTakeOut = true;
        ChangeFinished();

        //리워드 지급
        foreach (Reward reward in questData.RewardList) 
        {
            if (reward.item.ItemName == "Gold")
            {
                GameManager.gameManager.IncreaseGold(reward.quantity);
            }
            else if(reward.item.ItemName == "Exp")
            {
                GameManager.gameManager.IncreaseExp(reward.quantity);
            }
            else if(reward.item.ItemName == "Crystal")
            {
                GameManager.gameManager.IncreaseCrystal(reward.quantity);
            }
            else
            {
                inventory.AcquireItem(reward.item, (int)reward.quantity);
            }
        }
        QuestManager.AllTakeOut -= this.TakeOut;

        QuestManager.questManager.CheckIsThereReward();
    }

    public void TakeOutEach()
    {
        isTakeOut = true;
        ChangeFinished();

        //리워드 지급
        foreach (Reward reward in questData.RewardList)
        {
            if (reward.item.ItemName == "Gold")
            {
                GameManager.gameManager.IncreaseGold(reward.quantity);
            }
            else if (reward.item.ItemName == "Exp")
            {
                GameManager.gameManager.IncreaseExp(reward.quantity);
            }
            else if (reward.item.ItemName == "Crystal")
            {
                GameManager.gameManager.IncreaseCrystal(reward.quantity);
            }
            else
            {
                inventory.AcquireItem(reward.item, (int)reward.quantity);
            }
        }
        gameControl.ShowRewardShowPanel();

        Image[] images = RewardShowPanelReward.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            Destroy(image.gameObject);
        }
        foreach (Reward reward in questData.RewardList)
        {
            GameObject obj = Instantiate(RewardSlot, RewardShowPanelReward);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = reward.item.itemImage;
            obj.transform.GetChild(2).GetComponent<Text>().text = $"{reward.quantity}";
        }
        QuestManager.questManager.CheckIsThereReward();
    }

    private void ChangeFinished()
    {
        finishImg.SetActive(true);
        this.transform.SetAsLastSibling();
    }

    //퀘스트가 달성되었는지 체크
    public void CheckComplete()
    {
        isCompleted=inventory.CheckItem(questData.questItem, questData.questItemQuantity);
        questItemCount = inventory.CheckItemCount(questData.questItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isCompleted) return;
        MiniItemInfo.miniItemInfo.ShowQuestInfo(questData, questItemCount);
        MiniItemInfo.miniItemInfo.transform.position = this.transform.position + Vector3.up * 0.5f + Vector3.right * 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MiniItemInfo.miniItemInfo.CloseItemInfo();
    }
}
