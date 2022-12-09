using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename = "";

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
        filename = Application.dataPath + "/test.csv";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            WriteCSV();
    }

    public void WriteCSV()
    {
        if(myPlayerList.player.Length>0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name, Health, Damage, Defense");
            tw.Close();

            tw = new StreamWriter(filename, true);
            for(int i=0; i<myPlayerList.player.Length;i++)
            {
                tw.WriteLine(myPlayerList.player[i].name + "," + myPlayerList.player[i].health + "," +
                    myPlayerList.player[i].damage + "," + myPlayerList.player[i].defence);
            }
            tw.Close();
        }
    }

}
