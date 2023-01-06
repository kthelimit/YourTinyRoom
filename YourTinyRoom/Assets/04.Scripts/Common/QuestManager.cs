using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    public Quest[] quests;
    public List<QuestInList> questInLists;
    public GameObject questItem;
    public Transform scrollContents;
    public Button allReceiveBtn;
    public GameObject alarmImage;

    //이벤트를 이용해 일괄수령 기능 추가
    public delegate void AllReceiveHandler(); //반환형이 보이드인 매개변수가 없는 함수를 대상으로 함.
    public static event AllReceiveHandler AllTakeOut;//이벤트화


    void Awake()
    {
        if(questManager==null)
            questManager = this;
        quests = Resources.LoadAll<Quest>("Quest");
        questInLists = new List<QuestInList>();

        foreach(Quest quest in quests)
        {
            QuestInList questInList = new QuestInList();
            questInList.quest = quest;
            questInList.isTakeOut = false;
            questInList.isCompleted = false;
            questInList.isAlarmed = false;
            questInLists.Add(questInList);
        }

        foreach (QuestInList questIL in questInLists)
        {
            GameObject _questItem=Instantiate(questItem, scrollContents);
            _questItem.GetComponent<QuestSlot>().SetQuest(questIL);
        }

        CheckIsThereReward();
    }

    public void LoadQuestList(List <QuestInList> _list)
    {
        questInLists = _list;
    }



    public void CheckIsThereReward()
    {
        QuestSlot[] questItems = scrollContents.GetComponentsInChildren<QuestSlot>();
        int checkReward = 0;
        foreach(QuestSlot _quest in questItems)
        {
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
        AllTakeOut();
        allReceiveBtn.interactable = false;
        alarmImage.SetActive(false);
        CheckIsThereReward();
    }
}
