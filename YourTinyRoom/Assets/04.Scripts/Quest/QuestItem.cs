using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
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

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        scrollContents = this.transform.parent;
        rewardButton = transform.GetChild(0).GetComponent<Button>();
        rewardButton.interactable = false;
        questTitle.text = questData.questName;
        questDesc.text = questData.questDesc;
        StartCoroutine("UpdateCheck");
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
        rewardButton.interactable = true;
        //To do list : 화면에 퀘스트를 달성했다는 알림 주기 및 수령할 것이 있다는 것을 메뉴에 표시해주기.
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

    }
    //퀘스트가 달성되었는지 체크
    public void CheckComplete()
    {
        isCompleted=inventory.CheckItem(questData.questItem, questData.questItemQuantity);
    }
}
