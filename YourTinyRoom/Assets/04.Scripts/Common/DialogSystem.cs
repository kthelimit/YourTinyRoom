using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;

public class DialogSystem : MonoBehaviour
{
    
    public static DialogSystem dialogSystem;
    public Text WhoText;
    public Text content;
    Dictionary<int, string[]> talkData;
    public int talkIndex;
    public bool isAction = false;
    public int currentEventId;
    public GameControl gameControl;
    CharacterCtrl characterCtrl;
    public bool IsEvent = false;
    public List<DialogEvent> dialogEvents;
    
    private void Awake()
    {
        dialogSystem = this;
        WhoText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        content = transform.GetChild(0).GetComponent<Text>();
        talkData = new Dictionary<int, string[]>();
        dialogEvents = new List<DialogEvent>();
        characterCtrl = GameObject.Find("character").transform.GetComponent<CharacterCtrl>();
        GenerateData();
    }
    private void Start()
    {
        if(!dialogEvents[0].isFinished)
            Invoke("OpeningEvent", 0.1f);
    }



    private void OpeningEvent()
    {
        gameControl.OpenDialog(true);
        IsEvent = true;
        Talk(100);
 
    }
    
    private void SecondEvent()
    {        
        Talk(200);
        gameControl.OpenDialog(true);
    }

    void GenerateData()
    {
        talkData.Add(100, new string[] { "(여기는 내가 관리하는 작은 공간이다.):2", "(나 외엔 아무도 없어서 조금 쓸쓸하다 생각하고 있었지만 그래도 그럭저럭 괜찮게 지내고 있었는데…):2","(어느 날부터인가 갑자기 차원먼지가 생겨나기 시작했다.):2", "먼지 생기는 이벤트:9","큰일인데… 이 차원먼지, 내 힘으로는 제거할 수가 없어!:2", "이대로 가다간 이 공간이 먼지로 가득차게 될 거야!:2", "그렇지만 여긴 나 외엔 아무도 없어서 누군가에게 도와달라고도 할 수 없어…:2", "저기~:0", "어떻게 하지?:2", "저기~! 아까부터 부르고 있었는데~!!:0", "곤란한 거 같은데 내가 도와줄까?:0", "어..? :2", "도움을 필요로 하는 소리가 들려서 와 봤어!:0", "그, 그렇구나. 미안, 여기 누가 올거라고 생각도 못해서… 환청인 줄 알았어. 그런데 넌 누구야?:2", "아, 내 이름은...:0", "이름 입력 이벤트:8", "… 그게 네 이름이구나.:4","멋진 이름이네. 도와준다니 고마워.:2", "그러면 없애고 싶은 먼지를 눌러서 나한테 제거해달라고 부탁해봐.:1", "방문 이벤트:7" });

        talkData.Add(200, new string[] { "고마워! 덕분에 많이 깨끗해졌어! 그동안 곤란했는데 정말 고마워.:2", "그러고보니… 줄곧 먼지에 가려져서 안 보이는 건가 싶었는데 사실 이 공간이 너였던 거야?:1", "맞아. 너 정말 예리하구나. 나는 이 공간을 관리하는 정령이야. 아직 힘이 약해서 먼지를 치울 수 없었어…:2", "앗 그런데 피곤하지는 않아?:2", "으음 조금..? 하나 정도 더 치우면 쉬러 가야할 거 같아. 하지만 아직도 먼지가 많이 남았는데…:1", "그렇지! 내가 다음에 또 놀러와서 청소해줄게.:1", "다시 놀러와 줄 거야?:2", "그럼! 내가 안 놀러오면 먼지로 뒤덮여서 큰일이 날지도 모르는걸!:1", "그리고 너랑 대화하는 것도 즐겁고…:1", "그러니까 다음에 또 놀러올게!:1", "연락처 교환하자~!:1", "(이후 캐릭터 메뉴에서 하루에 한 번, 상대를 초대할 수 있습니다.):5", "이벤트 종료:6" });
        talkData.Add(300, new string[] { "(상점에 새로운 아이템이 추가되었다!):5", "이벤트 종료:6" });
        
        
        foreach (var pair in talkData)
        {
            DialogEvent dialogEvent = new DialogEvent();
            dialogEvent.EventNumber = pair.Key;
            dialogEvent.isFinished = false;
            dialogEvents.Add(dialogEvent);
        }
    }

    private void Update()
    {
        if(GameManager.gameManager.DustRemoveCount==2&&!dialogEvents[1].isFinished)
        {
            SecondEvent();
        }
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
    public void NextSentence()
    {
        if (!isAction) return;
        Talk(currentEventId);

    }

    public void Talk(int id)
    {
        for(int i=0;i<dialogEvents.Count;i++)
        {
            if(dialogEvents[i].EventNumber==id)
            {
                dialogEvents[i].isFinished = true;
            }
        }
        string tData = GetTalk(id, talkIndex);
        currentEventId = id;
        if (tData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        string talkText = tData.Split(':')[0];
        int talkWho = int.Parse(tData.Split(':')[1]);

        if (talkWho == 0)
        {
            SomeoneTalk("???", talkText);
        }
        else if (talkWho == 1)
        {
            CharacterTalk(talkText);
        }
        else if (talkWho == 2)
        {
            MyTalk(talkText);
        }
        else if(talkWho==4)
        {
            talkText = $"{GameManager.gameManager.characterName}" + talkText;
            MyTalk(talkText);
        }
        else if(talkWho==5)
        {
            SomeoneTalk("-", talkText);
        }
        else if (talkWho == 6)
        {
            gameControl.OpenDialog(false);            
            isAction = false;
            talkIndex = 0;
            IsEvent = false;
            return;
        }
        else if (talkWho == 7)
        {
            characterCtrl.CallVisitEvent();
            gameControl.OpenDialog(false);
            isAction = false;
            talkIndex = 0;
            return;
        }
        else if (talkWho == 8)
        {
            gameControl.ShowCNamedEditPanelForEvent();
        }
        else if (talkWho == 9)
        {
            GameManager.gameManager.Invoke("MakeDust", 1f);
            GameManager.gameManager.Invoke("MakeDust", 2f);
            GameManager.gameManager.Invoke("MakeDust", 3f);
            GameManager.gameManager.Invoke("MakeDust", 4f);
            GameManager.gameManager.Invoke("MakeDust", 5f);
        }

        isAction = true;
        talkIndex++;
    }
    private void SomeoneTalk(string name, string str)
    {
        WhoText.text = name;
        content.text = str;
    }


    private void CharacterTalk(string str)
    {
        WhoText.text = GameManager.gameManager.characterName;
        content.text = str;
    }

    private void MyTalk(string str)
    {
        WhoText.text = GameManager.gameManager.playerName;
        content.text = str;
    }

}
