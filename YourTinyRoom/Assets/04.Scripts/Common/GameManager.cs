using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;//싱글턴

    //자원관리
    private float gold =10000f;
    private Text goldText;
    private float crystal =100f;
    private Text crystalText;
    //날짜
    private float dayCount =0;
    private Text dayCountText;
    //경험치. 추후 레벨 테이블 구성 및 단계 구현하기
    private float curExp = 0f;
    private float maxExp = 100f;
    private Image expGauge;
   
    
   

    void Awake()
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        goldText = GameObject.Find("Canvas-UI").transform.GetChild(4).GetChild(0).GetChild(1).transform.GetComponent<Text>();
        crystalText= GameObject.Find("Canvas-UI").transform.GetChild(4).GetChild(1).GetChild(1).transform.GetComponent<Text>();
        dayCountText = GameObject.Find("Canvas-UI").transform.GetChild(2).GetChild(0).GetChild(5).transform.GetComponent<Text>();
        expGauge= GameObject.Find("Canvas-UI").transform.GetChild(2).GetChild(0).GetChild(7).GetChild(1).transform.GetComponent<Image>();

    }


    void Start()
    {
        goldText.text = gold.ToString("#,###");
        crystalText.text = crystal.ToString("#,###");
        dayCountText.text = dayCount.ToString();
        expGauge.fillAmount = curExp / maxExp;
    }

    public bool IsBuyable(float price, int priceType)
    {
        if(priceType==1)
        {
            if (price <= crystal)
                return true;
            else
                return false;
        }
        else
        {
            if (price <= gold)
                return true;
            else
                return false;
        }
    }


    public void IncreaseGold(float amount)
    {
        gold += amount;
        goldText.text = gold.ToString("#,###");
    }
    public void DecreaseGold(float amount)
    {
        gold -= amount;
        goldText.text = gold.ToString("#,###");
    }


    public void IncreaseCrystal(float amount)
    {
        crystal += amount;
        crystalText.text = crystal.ToString("#,###");
    }
    public void DecreaseCrystal(float amount)
    {
        crystal -= amount;
        crystalText.text = crystal.ToString("#,###");
    }

    public void IncreaseExp(float amount)
    {
        curExp += amount;
        expGauge.fillAmount = curExp / maxExp;
    }

    void Update()
    {

    }
}
