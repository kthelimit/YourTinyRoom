using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int level;
    public float expInterval;
    public float expLimit;
    public float rewardGold;
    public float rewardCrystal;
};
public class LevelTable : ScriptableObject
{
   public  List<Level> leveltable;
}
