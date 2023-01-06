using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

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
    private bool isAlarmed = false;
    private Transform canvasUI;
    public int questItemCount;
    Transform rewardPanel;
    GameControl gameControl;

    private void Awake()
    {
        rewardPanel = transform.GetChild(3);
        QuestAlarmPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/QuestAlarm.prefab", typeof(GameObject));
        QuestAlarmPanelText = QuestAlarmPrefab.transform.GetChild(0).GetComponent<Text>();
        rewardPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/RewardItem.prefab", typeof(GameObject));
        canvasUI = GameObject.Find("Canvas-UI").transform;
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        scrollContents = this.transform.parent;
        rewardButton = transform.GetChild(0).GetComponent<Button>();
        rewardButton.interactable = false;
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
        StartCoroutine("UpdateCheck");
    }

    public void SetReward()
    {
        GameObject reward1 = Instantiate(rewardPrefab, rewardPanel);
        reward1.GetComponentInChildren<Image>().sprite = questData.RewardItem1.itemImage;
        reward1.GetComponentInChildren<Text>().text = $"{questData.RewardQuantity1}";
       
        GameObject reward2 = Instantiate(rewardPrefab, rewardPanel);
        reward2.GetComponentInChildren<Image>().sprite = questData.RewardItem2.itemImage;
        reward2.GetComponentInChildren<Text>().text = $"{questData.RewardQuantity2}";

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
        finishImg.SetActive(true);
        this.transform.SetAsLastSibling();
        
        //리워드 지급
        if (questData.RewardItem1.name == "Gold")
            GameManager.gameManager.IncreaseGold(questData.RewardQuantity1);
        else if(questData.RewardItem1.name == "Exp")
            GameManager.gameManager.IncreaseExp(questData.RewardQuantity1);

        if (questData.RewardItem2.name == "Gold")
            GameManager.gameManager.IncreaseGold(questData.RewardQuantity2);
        else if (questData.RewardItem2.name == "Exp")
            GameManager.gameManager.IncreaseExp(questData.RewardQuantity2);
        QuestManager.AllTakeOut -= this.TakeOut;

        QuestManager.questManager.CheckIsThereReward();
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
