using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;

    [System.Serializable]
    public class Player
    {
        public string name;
        public int health;
        public int damage;
        public int defence;
    }
    [System.Serializable]
    public class PlayerList
    {
        public Player[] player;
    }
    public PlayerList myPlayerList = new PlayerList();



    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / 4 - 1;
        myPlayerList.player = new Player[tableSize];
        for (int i = 0; i < tableSize; i++)
        {
            myPlayerList.player[i] = new Player();
            myPlayerList.player[i].name = data[4 * (i + 1)];
            myPlayerList.player[i].health = int.Parse(data[4 * (i + 1) + 1]);
            myPlayerList.player[i].damage = int.Parse(data[4 * (i + 1) + 1]);
            myPlayerList.player[i].defence = int.Parse(data[4 * (i + 1) + 1]);

        }
    }
}
