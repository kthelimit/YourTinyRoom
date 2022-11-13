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
    private CanvasGroup canvasTimeBar; //타임게이지를 숨기는 용도
    public bool isComplete = false; //작물이 완성 되었는가?

    void Start()
    {
        curSprite = GetComponent<SpriteRenderer>();
        TimeBar = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
        curSprite.sprite = Sprites[0];
        completeTxt = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        completeTxt.enabled = false;
        canvasTimeBar = transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>();
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while (!isComplete)
        {
            
            curTime += 1f;
            TimeBar.fillAmount = curTime / completeTime;          

            for(int i=0; i<Sprites.Length-1;i++)
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

    private void OnMouseDown()
    {
        if(isComplete)//완성되었다면
        {
            Harvest();
        }
        //TO DO: 클릭시 남은 시간을 보여줄지에 대해서 고민중...
    }

    private void Harvest()
    {
        Debug.Log("수확!");
        Destroy(this.gameObject);
        //TO DO: 작물 아이콘이 인벤토리 방향으로 이동하는 효과
        //TO DO: 작물의 수량이 증가하는 코드
    }

    //TO DO: 맵의 정해진 위치에 심는 코드
    //드래그 앤 드롭 / 클릭해서 작물 선택 후 원하는 위치를 클릭해 한번에 다량으로 심는 기능
}
