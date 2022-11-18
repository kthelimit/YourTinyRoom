using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    [SerializeField]
    private Button rewardButton;
    private Transform scrollContents;
    public bool isCompleted = false;
    public bool isTakeOut = false;
    public GameObject finishImg;
    


    private void Start()
    {
        scrollContents = this.transform.parent;
        rewardButton = transform.GetChild(0).GetComponent<Button>();
        rewardButton.interactable = false;
        
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
        finishImg.SetActive(true);
        this.transform.SetParent(scrollContents.parent);
        this.transform.SetParent(scrollContents);
    }
}
