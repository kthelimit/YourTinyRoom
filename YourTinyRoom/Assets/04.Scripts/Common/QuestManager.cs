using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    public Quest[] questlist;
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
        questlist = Resources.LoadAll<Quest>("Quest");

        foreach (Quest quest in questlist)
        {
            GameObject _questItem=Instantiate(questItem, scrollContents);
            _questItem.GetComponent<QuestItem>().questData = quest;
        }

        CheckIsThereReward();
    }




    public void CheckIsThereReward()
    {
        QuestItem[] questItems = scrollContents.GetComponentsInChildren<QuestItem>();
        int checkReward = 0;
        foreach(QuestItem _quest in questItems)
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
