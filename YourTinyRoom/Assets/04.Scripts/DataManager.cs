using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager
{
    private static DataManager instance;
    public Dictionary<int, Item> dicItem;
    private DataManager() { }

    public static DataManager GetInstance()
    {
        if(DataManager.instance==null)
        {
            DataManager.instance = new DataManager();
        }
        return DataManager.instance;
    }

}
