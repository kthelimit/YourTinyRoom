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
    public Image likeBar2;
    public Image likeBarOnHead;
    public GameObject likeBarPanelOnHead;

    public float energyParameter=0f;
    private float energyMax = 50f;
    public Image energyBar;
    public Image energyBarOnHead;
    public GameObject energyBarPanelOnHead;

    //머리 위 바의 위치 조정용
    public Transform HeadOnBar;

    GameControl gameControl;
    public bool IsVisited = false;
    public Transform visit;
    public Button InviteBtn;

    //대사용 말풍선
    public GameObject textbubble;
    private Canvas textbubbleCanvas;
    private Text bubbletext;

    void Awake()
    {
        tr = GetComponent<Transform>();
        ani = GetComponent<SkeletonAnimation>();
        ChangeAnimation("대기");
        characterRenderer = GetComponentInChildren<Renderer>();
        textbubbleCanvas = textbubble.GetComponent<Canvas>();
        bubbletext = textbubble.GetComponentInChildren<Text>();
        textbubble.SetActive(false);
        energyParameter = energyMax;
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        UpdateEnergyBar();
        UpdateLikeBar();
        StartAtHome();
    }

    public void StartAtHome()
    {
        isHome = true;
        tr.position = home.position;
        IsVisited = false;
        energyParameter = energyMax;
        UpdateEnergyBar();
        gameControl.OpenCharacterVisit(false);
    }

    public void Invite()
    {
        if (isHome == false)
        {
            Debug.Log("지금 방문 중이야.");
            return;
        }
        else if (isHome && IsVisited)
        {
            Debug.Log("오늘은 이미 방문했어.");
            return;
        }
        StartCoroutine("Visit");
        InviteBtn.interactable = false;
    }

    IEnumerator Visit()
    {
        isReaction = true;
        gameControl.OpenCharacterVisit(true);
        gameControl.OpenCustomize(false);
        isHome = false;
        tr.position = visit.position;
        IsVisited = true;
        UpdateLikeBar(5f);
        Camera.main.transform.position = tr.position + new Vector3(0f, 0f, -10f);
        ChangeAnimation("안녕");
        yield return new WaitForSeconds(2f);
        Talk("놀러왔어~!!");
        yield return new WaitForSeconds(2f);
        Talk("초대해줘서 고마워!");
        yield return new WaitForSeconds(2f);
        ChangeAnimation("대기");
        yield return new WaitForSeconds(3f);
        isReaction = false;
        StartCoroutine("ChooseAction");
    }

    void Update()
    {
        characterRenderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
        textbubbleCanvas.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit)+1;
        if (energyParameter<=0f&&!isHome)
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
                ChangeAnimation("걷기");
                while (isMoving&&!isReaction)
                {
                    tr.position = Vector3.Lerp(tr.position, target, Time.deltaTime * moveSpeed);
                    HeadOnBar.position = tr.position;
                    yield return null;
                    if (Vector2.Distance(tr.position, target) < 0.1f)
                    {
                        isMoving = false;
                    }
                }
            }
            else
            {
                ChangeAnimation("대기");
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
            ChangeAnimation("깜짝");
            switch (idx)
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
        }
        else
        {
            ChangeAnimation("안녕");
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
                case 10:
                    Talk(GameManager.gameManager.playerName+"도 같이 놀고 싶어?");
                    break;
            }
            yield return new WaitForSeconds(2f);
        }
        ChangeAnimation("대기");
        isReaction = false;
        StartCoroutine("ChooseAction");
    }

    public void Talk(string message)
    {
        StartCoroutine(TalkBubble(message));
    }

    IEnumerator TalkBubble(string message)
    {
        textbubble.SetActive(true);
        textbubble.transform.position = tr.position + new Vector3(0f, 1.7f, 0f);
        bubbletext.text = message;
        yield return new WaitForSeconds(1f);
        textbubble.SetActive(false);
    }
    IEnumerator ByeBye()
    {
        energyBarPanelOnHead.SetActive(false);
        likeBarPanelOnHead.SetActive(false);
        isHome = true;
        isReaction = true;
        isMoving = false;
        Camera.main.transform.position = tr.position + new Vector3(0f, 0f, -10f);
        yield return new WaitForSeconds(1f);
        Talk("앗");
        ChangeAnimation("깜짝");
        yield return new WaitForSeconds(2f);
        Talk("이제 가볼게!");
        ChangeAnimation("대기");
        yield return new WaitForSeconds(2f);
        Talk("다음에 또 보자!");
        ChangeAnimation("안녕");
        yield return new WaitForSeconds(2f);
        tr.position = home.position;
        isReaction = false;
        gameControl.OpenCharacterVisit(false);
        ChangeAnimation("정지");
        StopCoroutine("ChooseAction");
    }

    public void ChangeAnimation(string str)
    {
        if (str == "대기")
        {
            ani.AnimationName = "Idle";
            ani.loop = true;
        }
        else if (str == "깜짝")
        {
            ani.AnimationName = "KKamchack";
            ani.loop = false;
        }
        else if (str == "안녕")
        {
            ani.AnimationName = "hello";
            ani.loop = false;
        }
        else if (str == "걷기")
        {
            ani.AnimationName = "walk";
            ani.loop = true;
        }
        else if(str=="정지")
        {
            ani.AnimationName = "Idle";
            ani.loop = false;
        }
    }
    public void PlayAnimation(string str)
    {
        isReaction = true;
        ChangeAnimation(str);
        StartCoroutine("PlayAnimation");
    }

    IEnumerator PlayAnimation()
    {
        StopCoroutine("ChooseAction");
        yield return new WaitForSeconds(2f);
        StartCoroutine("ChooseAction");
        isReaction = false;
    }
    public void UpdateLikeBar(float num = 0f)
    {
        likingParameter += num;
        StartCoroutine("LikeBarAnimation");
    }

    IEnumerator LikeBarAnimation()
    {
        HeadOnBar.position = tr.position;
        likeBarPanelOnHead.SetActive(true);
        while (true)
        {
            HeadOnBar.position = tr.position;
            likeBar.fillAmount = Mathf.Lerp(likeBar.fillAmount, likingParameter / likingMax, 0.1f);
            likeBar2.fillAmount = Mathf.Lerp(likeBar.fillAmount, likingParameter / likingMax, 0.1f);
            likeBarOnHead.fillAmount = Mathf.Lerp(likeBar.fillAmount, likingParameter / likingMax, 0.1f);
            yield return null;
            if (Mathf.Abs(likingParameter / likingMax - likeBar.fillAmount) < 0.01f)
                break;
        }
        likeBar.fillAmount = likeBar2.fillAmount = likeBarOnHead.fillAmount = likingParameter / likingMax;
       yield return new WaitForSeconds(1f);
        likeBarPanelOnHead.SetActive(false);

    }

    public void UpdateEnergyBar(float num = 0f)
    {
        energyParameter += num;
        StartCoroutine("EnergyBarAnimation");
    }

    IEnumerator EnergyBarAnimation()
    {
        HeadOnBar.position = tr.position;
        energyBarPanelOnHead.SetActive(true);

        while (true)
        {
            HeadOnBar.position = tr.position;
            energyBar.fillAmount = Mathf.Lerp(energyBar.fillAmount, energyParameter / energyMax, 0.1f);
            energyBarOnHead.fillAmount = Mathf.Lerp(energyBar.fillAmount, energyParameter / energyMax, 0.1f);
            yield return null;
            if (Mathf.Abs(energyParameter / energyMax - energyBar.fillAmount) < 0.01f)
                break;
        }
        energyBar.fillAmount = energyBarOnHead.fillAmount = energyParameter / energyMax;
        yield return new WaitForSeconds(1f);
        energyBarPanelOnHead.SetActive(false);
    }

}
