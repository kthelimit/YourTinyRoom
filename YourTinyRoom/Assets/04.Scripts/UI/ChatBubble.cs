using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    public Text ChatText;

    public void ChangeText(string msg)
    {
        ChatText.text = msg;
    }

}
