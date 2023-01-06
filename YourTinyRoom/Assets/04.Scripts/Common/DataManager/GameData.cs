using System.Collections;
using System.Collections.Generic;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public string PlayerName="플레이어";
        public string CharacterName = "캐릭터";
        public float Gold = 1000f;
        public float Crystal = 10f;
        public int Level = 1;
        public float Exp = 0f;
        public List<ItemInInventory> ItemInInventories = new List<ItemInInventory>();
        public List<CollectItem> CollectItems = new List<CollectItem>();
        public List<QuestInList> questInLists = new List<QuestInList>();
    }




}