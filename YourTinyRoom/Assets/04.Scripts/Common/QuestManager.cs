using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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

    //이벤트를 이용해 일괄수령 기능 추가
    public delegate void AllReceiveHandler(); //반환형이 보이드인 매개변수가 없는 함수를 대상으로 함.
    public static event AllReceiveHandler AllTakeOut;//이벤트화


    void Awake()
    {
        if (questManager == null)
            questManager = this;
        quests = Resources.LoadAll<Quest>("Quest");
        questInLists = new List<QuestInList>();




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
            Debug.Log("퀘스트 로딩중");
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
        AllTakeOut();
        allReceiveBtn.interactable = false;
        alarmImage.SetActive(false);
        CheckIsThereReward();
    }
}
