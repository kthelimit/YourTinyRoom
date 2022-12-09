using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] questlist;
    public GameObject questItem;
    public Transform scrollContents;
    void Awake()
    {
        questlist = Resources.LoadAll<Quest>("Quest");

        foreach (Quest quest in questlist)
        {
            GameObject _questItem=Instantiate(questItem, scrollContents);
            _questItem.GetComponent<QuestItem>().questData = quest;
        }
    }

    void Update()
    {
        
    }
}
