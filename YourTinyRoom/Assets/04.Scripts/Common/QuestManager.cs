using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using DataInfo;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    public Quest[] quests;
    public List<QuestInList> questInLists;
    public GameObject questItem;
    public Transform scrollContents;
    public Button allReceiveBtn;
    public GameObject alarmImage;

    public GameObject RewardShowPanel;
    public GameObject RewardSlot;
    public Transform RewardPanel;
    GameControl gameControl;

    //이벤트를 이용해 일괄수령 기능 추가
    public delegate void AllReceiveHandler(); //반환형이 보이드인 매개변수가 없는 함수를 대상으로 함.
    public static event AllReceiveHandler AllTakeOut;//이벤트화


    void Awake()
    {
        if (questManager == null)
            questManager = this;
        quests = Resources.LoadAll<Quest>("Quest");
        questInLists = new List<QuestInList>();

        RewardSlot = Resources.Load<GameObject>("Prefabs/RewardSlot");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/RewardSlot.prefab", typeof(GameObject));
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        CheckIsThereReward();
    }

    public void LoadQuestList(List <QuestInList> _list)
    {
        if(_list.Count==0)
        {
            Debug.Log("퀘스트 생성중");
            foreach (Quest quest in quests)
            {
                QuestInList questInList = new QuestInList();
                questInList.quest = quest;
                questInList.isTakeOut = false;
                questInList.isCompleted = false;
                questInList.isAlarmed = false;
                questInLists.Add(questInList);
            }
        }
        else if (_list.Count != 0)
        {
            foreach (QuestInList QIL in _list)
            {
                QuestInList questInList = new QuestInList();
                questInList.quest = QIL.quest;
                questInList.isTakeOut = QIL.isTakeOut;
                questInList.isCompleted = QIL.isCompleted;
                questInList.isAlarmed = QIL.isAlarmed;
                questInLists.Add(questInList);
            }
        }

        foreach (QuestInList questIL in questInLists)
        {
            GameObject _questItem = Instantiate(questItem, scrollContents);
            _questItem.GetComponent<QuestSlot>().SetQuest(questIL);
        }

    }

    public List<QuestInList> SaveQuestList()
    {        
        return questInLists;
    }


    public void CheckIsThereReward()
    {
        QuestSlot[] questItems = scrollContents.GetComponentsInChildren<QuestSlot>();
        int checkReward = 0;
        foreach(QuestSlot _quest in questItems)
        {

            for (int i = 0; i < questInLists.Count; i++)
            {
                if (_quest.questData == questInLists[i].quest)
                {
                    questInLists[i].isTakeOut = _quest.isTakeOut;
                    questInLists[i].isAlarmed = _quest.isAlarmed;
                    questInLists[i].isCompleted = _quest.isCompleted;
                }
            }

            if (_quest.isCompleted && !_quest.isTakeOut) 
            {
                allReceiveBtn.interactable = true;
                alarmImage.SetActive(true);
                checkReward++;
            }            
        }
        if(checkReward==0)
        {
            allReceiveBtn.interactable = false;
            alarmImage.SetActive(false);
        }
        
    }

    public void AllReceiveClick()
    {
        //퀘스트 리워드 합산을 위해 현재 수령 가능한 퀘스트 리스트를 LINQ로 뽑아옴
        QuestSlot[] questItems = scrollContents.GetComponentsInChildren<QuestSlot>();
        var takeRewards = from questItem in questItems
                          where questItem.isCompleted && !questItem.isTakeOut
                          select questItem;

        //퀘스트 리워드 합산용 리스트
        List<Reward> Rewards = new List<Reward>();

        //리워드가 중복되는지 확인 후 중복되지 않으면 새로 넣는다
        foreach(var takeReward in takeRewards)
        {          
            foreach(Reward re in takeReward.questData.RewardList)
            {
                bool isOverlap = false;
                if (Rewards.Count != 0)
                {
                    for (int i = 0; i < Rewards.Count; i++)
                    {
                        if(re.item==Rewards[i].item)
                        {
                            isOverlap = true;
                            Rewards[i].quantity += re.quantity;
                            break;
                        }
                    }
                }
                if(!isOverlap)
                {
                    Reward reward = new Reward();
                    reward.item = re.item;
                    reward.quantity = re.quantity;
                    Rewards.Add(reward);
                }
            }
        }
        gameControl.ShowRewardShowPanel();
        //합산된 리워드리스트를 이용해 화면에 리워드 내용을 띄워줌

        Image[] images = RewardPanel.GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            Destroy(image.gameObject);
        }
        foreach (Reward _reward in Rewards)
        {
            GameObject obj = Instantiate(RewardSlot, RewardPanel);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = _reward.item.itemImage;
            obj.transform.GetChild(2).GetComponent<Text>().text = $"{_reward.quantity}";
        }

        AllTakeOut();
        allReceiveBtn.interactable = false;
        alarmImage.SetActive(false);
        CheckIsThereReward();
    }
}


