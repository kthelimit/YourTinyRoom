using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    [SerializeField]
    private Button rewardButton;
    private Transform scrollContents;
    public Text QuestName;
    public Text QuestCondition;
    public bool isCompleted = false;
    public bool isTakeOut = false;
    


    private void Start()
    {
        scrollContents = this.transform.parent;
        rewardButton = transform.GetChild(0).GetComponent<Button>();
        QuestName = transform.GetChild(1).GetComponent<Text>();
        QuestCondition = transform.GetChild(3).GetComponent<Text>();
        rewardButton.interactable = false;
        
    }
    public void SetQuest(string questName, string questCondition)
    {
        QuestName.text = questName;
        QuestCondition.text = questCondition;
    }


    private void Update()
    {
        if (isCompleted && !isTakeOut) 
        {
            QuestComplete();
        }


    }

    public void QuestComplete()
    {      
        rewardButton.interactable = true;
    }

    public void TakeOut()
    {
        isTakeOut = true;

        Text rewardBtnText = rewardButton.transform.GetChild(0).GetComponent<Text>();
        rewardBtnText.text = "수령완료";
        rewardButton.interactable = false;
        ColorBlock colorBlock = rewardButton.colors;
        colorBlock.disabledColor = new Color(0.7f,0.18f,0.18f,1f);        
        rewardButton.colors = colorBlock;

        this.transform.SetParent(scrollContents.parent);
        this.transform.SetParent(scrollContents);
    }
}
