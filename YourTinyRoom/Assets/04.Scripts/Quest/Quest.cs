using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/quest")]

public class Quest : ScriptableObject
{      
    public int questID;
    public string questName;
    public string questDesc;
    public Item questItem;
    public int questItemQuantity;
    public List<Reward> RewardList;
}
