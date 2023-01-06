using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
//어튜리뷰트를 사용해서 자동 생성 
[CreateAssetMenu(fileName = "GameDataSO",
            menuName = "Create GameData", order = 1)]
//생성할 메뉴의 순서를 결정
public class GameDataObject : ScriptableObject
{
    public string PlayerName = "플레이어";
    public string CharacterName = "캐릭터";
    public float Gold = 1000f;
    public float Crystal = 10f;
    public int Level = 1;
    public float Exp = 0f;
    public List<ItemInInventory> ItemInInventories = new List<ItemInInventory>();
    public List<CollectItem> CollectItems = new List<CollectItem>();
    public List<QuestInList> questInLists = new List<QuestInList>();

}