using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/quest")]

public class QuestData : ScriptableObject
{      
    public int questID;
    public string questName;
    public string questDesc;
    public Item questItem;
    public int questItemQuantity;
    public Item RewardItem1;
    public float RewardQuantity1;
    public Item RewardItem2;
    public float RewardQuantity2;





}
