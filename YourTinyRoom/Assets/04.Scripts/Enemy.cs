using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Enemy", menuName ="Assets/New Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int hp;
    public int strength;
    public int xpReward;
}
