using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;//싱글턴

    //자원관리
    public int gold=10000;
    private Text goldText;
    public int crystal=0;
    private Text crystalText;
    public int dayCount=0;
    private Text dayCountText;
   
    
   

    void Awake()
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        goldText = GameObject.Find("Canvas-UI").transform.GetChild(4).GetChild(0).GetChild(1).transform.GetComponent<Text>();
        crystalText= GameObject.Find("Canvas-UI").transform.GetChild(4).GetChild(1).transform.GetComponent<Text>();
        dayCountText = GameObject.Find("Canvas-UI").transform.GetChild(2).GetChild(0).GetChild(5).transform.GetComponent<Text>();


    }


    void Start()
    {
        goldText.text = gold.ToString("#,###");
        crystalText.text = crystal.ToString("#,###");
        dayCountText.text = dayCount.ToString() + "일째";
    }




    void IncreaseGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString("#,###");
    }
    void DecreaseGold(int amount)
    {
        gold -= amount;
        goldText.text = gold.ToString("#,###");
    }


    void IncreaseCrystal(int amount)
    {
        crystal += amount;
        crystalText.text = crystal.ToString("#,###");
    }
    void DecreaseCrystal(int amount)
    {
        crystal -= amount;
        crystalText.text = crystal.ToString("#,###");
    }



    void Update()
    {

    }
}
