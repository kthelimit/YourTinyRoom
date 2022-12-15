using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour
{
    public RectTransform contentsList;
    public int count;
    private float pos;
    private bool isScroll = false;
    private float moveDist;
    private float moveLimitRight;
    private float moveLimitLeft;
    private float offsetY;

    void Awake()
    {
        pos = contentsList.localPosition.x;
        count = contentsList.childCount;
        moveDist= contentsList.GetComponent<GridLayoutGroup>().cellSize.x + contentsList.GetComponent<GridLayoutGroup>().spacing.x;
        float contentWidth = contentsList.GetComponent<GridLayoutGroup>().cellSize.x * count + contentsList.GetComponent<GridLayoutGroup>().spacing.x * (count - 1);
        moveLimitLeft = contentsList.rect.xMin+ contentsList.localPosition.x;
        moveLimitRight = moveLimitLeft - (contentWidth - contentsList.rect.width);
        offsetY = GetComponent<RectTransform>().sizeDelta.y / 2;
    }

    public void Right()
    {
        pos = contentsList.localPosition.x;

        if (pos <= moveLimitRight) return;

        isScroll = true;
        pos -= moveDist;
        StartCoroutine("scroll");


    }

    public void Left()
    {
        pos = contentsList.localPosition.x;

        if (pos >= moveLimitLeft) return;

        isScroll = true;
        pos += moveDist;
        StartCoroutine("scroll");

    }


    IEnumerator scroll()
    {
        while(isScroll)
        {
            if(Vector2.Distance(contentsList.localPosition, new Vector2(pos, offsetY))<0.1f)
            {
                isScroll = false;
            }
            contentsList.localPosition = Vector2.Lerp(contentsList.localPosition, new Vector2(pos, offsetY), Time.deltaTime * 5);
            
            yield return null;
        }
    }
}
