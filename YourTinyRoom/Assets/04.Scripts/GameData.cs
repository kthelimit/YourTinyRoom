using System.Collections;
using System.Collections.Generic;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        
    }

    [System.Serializable]
    public class Item
    {
        public enum ItemType { HP, EXP, COLLECT, PRESENT}
        public enum ItemCalc { INC_VALUE, PERCENT,NONE}
        public ItemType itemType;
        public ItemCalc itemCalc;
        public string name;
        public string desc;
        public float value;
    }
}