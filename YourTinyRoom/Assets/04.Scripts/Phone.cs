using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Phone : MonoBehaviour
{
    public Transform Content;
    private GameObject ChatBubbleCharaPrefab;
    private GameObject ChatBubbleMyPrefab;

    void Awake()
    {
        ChatBubbleCharaPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/ChatBubble.prefab", typeof(GameObject));
        ChatBubbleMyPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/05.Prefabs/ChatBubble2.prefab", typeof(GameObject));
        TestChat();
    }

    public void TestChat()
    {
        CharacterTalk("놀러가도 돼?");
        MyTalk("놀러와도 되지.\n기다릴게.");
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


}
