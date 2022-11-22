using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Crop : MonoBehaviour
{
    public float completeTime = 30f; //작물 완성되는 시간
    public float curTime = 0f;
    public Sprite[] Sprites;    //작물 스프라이트. 작물마다 수동으로 넣는다.
    private Image TimeBar;   //작물이 완성되기까지 얼마나 남았는지 보여주기 위한 타임게이지
    private SpriteRenderer curSprite;  
    private Text completeTxt; //완성시 출력되는 텍스트  
    private Text timeTxt; //시간 출력용 텍스트
    private CanvasGroup canvasTimeBar; //타임게이지를 숨기는 용도
    public bool isComplete = false; //작물이 완성 되었는가?
    public int quantity = 1;
    Inventory inventory;

    void Start()
    {
        curSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        TimeBar = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>();
        curSprite.sprite = Sprites[0];
        completeTxt = transform.GetChild(1).GetChild(1).GetComponent<Text>();
        completeTxt.enabled = false;
        timeTxt= transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<Text>();
        timeTxt.enabled = false;
        inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
        canvasTimeBar = transform.GetChild(1).GetChild(0).GetComponent<CanvasGroup>();
        StartCoroutine("Timer");

    }

    IEnumerator Timer()
    {
        while (!isComplete)
        {
            
            curTime += 1f;
            TimeBar.fillAmount = curTime / completeTime;
            float leftTime = completeTime - curTime;
            timeTxt.text = ((int)leftTime / 60 % 60).ToString("00") +":"+ ((int)leftTime % 60).ToString("00");


            for (int i=0; i<Sprites.Length-1;i++)
            {
                if (TimeBar.fillAmount > (float)i / (Sprites.Length-1))
                    curSprite.sprite = Sprites[i];            
            }                     

            if (curTime>=completeTime)
            {
                curSprite.sprite = Sprites[Sprites.Length - 1];
                canvasTimeBar.alpha = 0f;
                completeTxt.enabled = true;
                isComplete = true;
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void ShowLeftTime()
    {
        timeTxt.enabled = !timeTxt.enabled;
    }

    //TO DO: 맵의 정해진 위치에 심는 코드
    //드래그 앤 드롭 / 클릭해서 작물 선택 후 원하는 위치를 클릭해 한번에 다량으로 심는 기능
}
