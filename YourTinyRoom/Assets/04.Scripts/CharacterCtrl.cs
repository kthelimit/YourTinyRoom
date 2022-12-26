using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class CharacterCtrl : MonoBehaviour
{
    Transform tr;
    SkeletonAnimation ani;

    //이동반경 설정
    float h, v;
    public float hMax = 1;
    public float hMin = -1;
    public float vMax = 1;
    public float vMin = -1;
    Vector2 target;
    public float moveSpeed = 1f;

    int randomAction;
    public bool isMoving = false;
    public bool isReaction = false;
    public bool isHome = false;


    //y좌표로 출력 순서 조정
    public Renderer characterRenderer;
    private const int IsometricRangePerYUnit = 100;
   
    //캐릭터 방문 관련
    public Transform home;
    private float likingParameter=0f;
    private float likingMax=100f;
    public Image likeBar;
    public float energyParameter=0f;
    private float energyMax = 50f;
    public Image energyBar;
    GameControl gameControl;

    //대사용 말풍선
    public GameObject textbubble;
    private Text bubbletext;

    void Awake()
    {
        tr = GetComponent<Transform>();
        ani = GetComponent<SkeletonAnimation>();
        ani.AnimationName = "Idle";
        ani.loop = true;
        StartCoroutine("ChooseAction");
        characterRenderer = GetComponentInChildren<Renderer>();
        bubbletext = textbubble.GetComponentInChildren<Text>();
        textbubble.SetActive(false);
        energyParameter = energyMax;
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        UpdateEnergyBar();
        UpdateLikeBar();
    }

    void Update()
    {
        characterRenderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
        if(energyParameter<=0f&&!isHome)
        {
            StartCoroutine("ByeBye");
        }
    }

    IEnumerator ChooseAction()
    {
        while (true)
        {
            randomAction = Random.Range(0, 3);
            if (randomAction == 0)
            {
                MakeMovePoint();
                isMoving = true;
                ani.AnimationName = "walk";
                ani.loop = true;
                while (isMoving&&!isReaction)
                {
                    tr.position = Vector3.Lerp(tr.position, target, Time.deltaTime * moveSpeed);
                    yield return null;
                    if (Vector2.Distance(tr.position, target) < 0.1f)
                    {
                        isMoving = false;
                    }
                }
            }
            else
            {
                ani.AnimationName = "Idle";
                ani.loop = true;
                yield return new WaitForSeconds(Random.Range(1f, 5f));
            }
        }
    }


    private void MakeMovePoint()
    {
        h = Random.Range(hMin, hMax);
        v = Random.Range(vMin, vMax);
        target = new Vector3(h - v, h + v);

        if (target.x < tr.position.x)
        {
            tr.eulerAngles=new Vector3(0f, 0f, 0f);
        }
        else
        {
            tr.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }


    public IEnumerator Reaction()
    {
        StopCoroutine("ChooseAction");
        int idx=Random.Range(0,11);
        isReaction = true;
        if (idx%2==1)
        {
            ani.AnimationName = "KKamchack";
            ani.loop = false;
            switch(idx)
            {
                case 1:
                    Talk("우와앗!");
                    break;
                case 3:
                    Talk("까, 깜짝이야!");
                    break;
                case 5:
                    Talk("????????");
                    break;
                case 7:
                    Talk("꺄악!!");
                    break;
                case 9:
                    Talk("!!!!!!!!!");
                    break;
            }

            yield return new WaitForSeconds(2f);
            textbubble.SetActive(false);
        }
        else
        {
            ani.AnimationName = "hello";
            ani.loop = false;
            switch (idx)
            {
                case 0:
                    Talk("안녕~!");
                    break;
                case 2:
                    Talk("헤헷");
                    break;
                case 4:
                    Talk("무슨 일이야?");
                    break;
                case 6:
                    Talk("나도 반가워~");
                    break;
                case 8:
                    Talk("간지러워~");
                    break;
            }
            yield return new WaitForSeconds(2f);
            textbubble.SetActive(false);
        }
        ani.AnimationName = "Idle";
        ani.loop = true;
        isReaction = false;
        StartCoroutine("ChooseAction");
    }

    private void Talk(string message)
    {
        textbubble.SetActive(true);
        textbubble.transform.position = tr.position+new Vector3(-0.12f, 2.32f,0f);
        bubbletext.text = message;
    }

    IEnumerator ByeBye()
    {
        isHome = true;
        isReaction = true;
        yield return new WaitForSeconds(1f);
        Talk("앗");
        ani.AnimationName = "KKamchack";
        ani.loop = false;
        yield return new WaitForSeconds(2f);
        Talk("이제 가볼게!");
        ani.AnimationName = "Idle";
        ani.loop = true;
        yield return new WaitForSeconds(2f);
        Talk("다음에 또 보자!");
        ani.AnimationName = "hello";
        ani.loop = false;
        yield return new WaitForSeconds(2f);
        textbubble.SetActive(false);
        tr.position = home.position;
        isReaction = false;
        gameControl.OpenCharacterVisit(false);
        StopCoroutine("ChooseAction");
    }

    private void UpdateLikeBar()
    {
        likeBar.fillAmount = likingParameter / likingMax;
    }
    private void UpdateEnergyBar()
    {
        energyBar.fillAmount = energyParameter / energyMax;
    }
}
