using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour
{
    public RectTransform contentsList;
    public int count;
    private float pos;
    private float movepos;
    private bool isScroll = false;
    private float moveDist;
    private float moveLimitRight;
    private float moveLimitLeft;

    void Awake()
    {
        pos = contentsList.localPosition.x;
        movepos = contentsList.rect.xMax - contentsList.rect.xMax / count;
        count = contentsList.childCount;
        moveDist= contentsList.GetComponent<GridLayout>().cellSize.x + contentsList.GetComponent<GridLayout>().cellGap.x;
        Debug.Log(moveDist); //44
        moveLimitLeft = contentsList.rect.xMin - moveDist;
        moveLimitRight = contentsList.rect.xMax;

    }

    public void Right()
    {
        pos = contentsList.localPosition.x;
        if (pos >= moveLimitRight)
        {
            isScroll = false;

        }
        else
        {
            isScroll = true;
            movepos = pos - moveDist;
            Debug.Log(movepos);
            pos = movepos;
            StartCoroutine("scroll");
        }
        
    }

    public void Left()
    {
        pos = contentsList.localPosition.x;
        if (pos <= 0f)
        {
            isScroll = false;
            pos = 0f;
        }
        else
        {
            isScroll = true;
            movepos = pos + moveDist;
            pos = movepos;
         //   StartCoroutine("scroll");
        }
    }


    IEnumerator scroll()
    {
        while(isScroll)
        {
            contentsList.localPosition = Vector2.Lerp(contentsList.localPosition, new Vector2(movepos, 40), Time.deltaTime * 5);
            if(Vector2.Distance(contentsList.localPosition, new Vector2(movepos,40))<0.1f)
            {
                isScroll = false;
            }
            yield return null;
        }
    }
}
