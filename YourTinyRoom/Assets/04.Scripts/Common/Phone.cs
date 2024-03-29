﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Phone : MonoBehaviour
{
    public Transform Content;
    private GameObject ChatBubbleCharaPrefab;
    private GameObject ChatBubbleMyPrefab;
    private GameObject StartChatPrefab;
    private GameObject EndChatPrefab;
    Dictionary<int, string[]> talkData;
    public int talkIndex;
    public bool isAction=false;
    public int currentEventId;
    public GameObject ButtonPanel;
    public Button Button1;
    public Button Button2;
    public ScrollRect scrollRect;
    public GameObject ImageAlarm;
    public GameObject phoneHelp;

    public AudioClip talkSfx;

    CharacterCtrl characterCtrl;
    GameControl gameControl;

    void Awake()
    {
        talkSfx = Resources.Load<AudioClip>("SFX/Power_Up_Bar_01");
        gameControl = GameObject.Find("GameControl").transform.GetComponent<GameControl>();
        characterCtrl = GameObject.FindWithTag("CHARACTER").transform.GetComponent<CharacterCtrl>();
        talkData = new Dictionary<int, string[]>();
        StartChatPrefab = Resources.Load<GameObject>("Prefabs/StartChat");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/StartChat.prefab", typeof(GameObject));
        ChatBubbleCharaPrefab = Resources.Load<GameObject>("Prefabs/ChatBubble");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/ChatBubble.prefab", typeof(GameObject));
        ChatBubbleMyPrefab = Resources.Load<GameObject>("Prefabs/ChatBubble2");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/ChatBubble2.prefab", typeof(GameObject));
        EndChatPrefab = Resources.Load<GameObject>("Prefabs/EndChat");
        //(GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/EndChat.prefab", typeof(GameObject));
        GenerateData();
        ButtonPanel.SetActive(false);  

    }

    void GenerateData()
    {
        talkData.Add(100, new string[] { "시작표시:0","놀러가도 돼?:1","지금?:2","안돼?:1","놀러와./지금은 좀...:9"});
        talkData.Add(101, new string[] { "놀러와.:2", "야호! 고마워! 금방 갈게~!!:1","방문이벤트:8"});
        talkData.Add(102, new string[] { "지금은 좀...:2", "그러면 어쩔 수 없지! 다음에 놀러가게 해줘~!:1", "대화종료:5" });

        talkData.Add(110, new string[] { "시작표시:0", "나 궁금한게 있어.:1", "뭔데?:2", "내가 없는 동안에는 주로 뭘 해?:1", "그냥 가만히 있어./이것저것?:9" });
        talkData.Add(111, new string[] { "그냥 가만히 있어:2", "그래? 심심하진 않아?:1", "내가 자주 놀러갈게:1", "대화종료:5" });
        talkData.Add(112, new string[] { "이것저것?:2", "주로 어떤 거?:1", "방정리/쇼핑:9" });
        talkData.Add(113, new string[] { "방 정리해.:2", "어쩐지! 지난번보다 깨끗해진 거 같더라!:1", "호감도이벤트:7" });
        talkData.Add(114, new string[] { "쇼핑을 해.:2", "뭐 샀어? 나한테도 알려줘:1", "비밀이야./너 줄 선물.:9" });
        talkData.Add(115, new string[] { "비밀이야:2", "그러지말고 가르쳐줘~!:1", "다음에 오면 알려줄게.:2", "좋아! 약속한 거야~!!:1", "호감도이벤트:7" });
        talkData.Add(116, new string[] { "너 줄 선물.:2", "정말? 뭐 줄지 기대된다!:1", "나도 뭔가 선물해줄테니까 기대해!:1", "호감도이벤트:7" });

        talkData.Add(120, new string[] { "시작표시:0", "컨디션이 안 좋은 거 같아...:1", "괜찮아?:2", "그래서 오늘은 놀러가지 못할거 같아:1", "걱정하지 말고 푹 쉬어:2", "방문불가이벤트:6" });

        talkData.Add(130, new string[] { "시작표시:0", "오늘은 우울한 일이 있었어...:1", "무슨 일인데?/괜찮아?:9" });
        talkData.Add(131, new string[] { "무슨 일인데?:2", "오늘 하루 종일 일이 잘 안 풀렸어...:1", $"그래도 {GameManager.gameManager.playerName}의 메세지를 보니까 기운이 나는 거 같아.:1", "그렇다면 다행이야.:2", "호감도이벤트:7" });
        talkData.Add(132, new string[] { "괜찮아?:2", $"걱정해주는 거야, {GameManager.gameManager.playerName}?:1", "당연하지:2", $"아까까진 우울했는데 {GameManager.gameManager.playerName}하고 대화하다보니까 좀 괜찮아진거 같아.:1", "그럼 다행이고.:2", $"{GameManager.gameManager.playerName}도 힘든 일 있으면 나한테 말해줘. 내가 큰 도움은 못 줘도 이야기 들어주는 건 잘 할 수 있어.:1", "호감도이벤트:7" });



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
        ImageAlarm.SetActive(false);
        string tData = GetTalk(id, talkIndex);
        currentEventId = id;
        if(tData==null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        SoundManager.soundManager.PlaySfx(this.transform.position, talkSfx);
        string talkText = tData.Split(':')[0];
        int talkWho = int.Parse(tData.Split(':')[1]);
        if (talkWho == 0)
        {
            Instantiate(StartChatPrefab, Content);
        }
        else if(talkWho==1)
        {
            CharacterTalk(talkText);
        }
        else if(talkWho == 2)
        {
            MyTalk(talkText);
        }
        else if (talkWho == 6)
        {
            characterCtrl.InviteBtn.interactable = false;
            Instantiate(EndChatPrefab, Content);
        }
        else if (talkWho == 7)
        {
            characterCtrl.UpdateLikeBar(10f);
            Instantiate(EndChatPrefab, Content);
        }
        else if(talkWho == 8)
        {
            if (characterCtrl.IsVisited)
            {
                CharacterTalk("아, 맞다. 아까도 놀러갔었지. 다음에 또 놀러갈게~!");
                Instantiate(EndChatPrefab, Content);
            }
            else
            {
                characterCtrl.Invite();
                isAction = false;
                talkIndex = 0; 
                Instantiate(EndChatPrefab, Content);
                gameControl.OpenPhone(false);
                return;
            }
        }
        else if(talkWho==9)
        {
            ButtonPanel.SetActive(true);
            Button1.GetComponentInChildren<Text>().text = talkText.Split('/')[0];
            Button2.GetComponentInChildren<Text>().text = talkText.Split('/')[1];
            isAction = false;
            talkIndex = 0;
            return;
        }
        else if(talkWho==5)
        {
            Instantiate(EndChatPrefab, Content);
        }
        isAction = true;
        talkIndex++;
        StartCoroutine("ScrollDown");
    }

    IEnumerator ScrollDown()
    {
        float waitTime = 0;
        while (true)
        {
            waitTime += 1;
            yield return null;
            scrollRect.verticalNormalizedPosition = 0f;
            if (waitTime >= 2)
                break;
        }
    }
    public void ClickButton1()
    {
        currentEventId+=1;
        Talk(currentEventId);
        ButtonPanel.SetActive(false);
    }
    public void ClickButton2()
    {
        currentEventId+=2;
        Talk(currentEventId);
        ButtonPanel.SetActive(false);
    }

    public void CharacterTalk(string msg)
    {
        GameObject obj = Instantiate(ChatBubbleCharaPrefab, Content);
        obj.GetComponent<ChatBubble>().ChangeText(msg);
    }

    public void MyTalk(string msg)
    {
        GameObject obj = Instantiate(ChatBubbleMyPrefab, Content);
        obj.GetComponent<ChatBubble>().ChangeText(msg);
    }

    public void ShowHelp(bool isOpen)
    {
        phoneHelp.SetActive(isOpen);
    }
}
