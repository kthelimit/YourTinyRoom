using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterCtrl : MonoBehaviour
{
    Transform tr;
    SkeletonAnimation ani;
    float h, v;
    Vector2 target;
    float dist=0f;
    int randomAction;
    float startTime;
    float idleTime;

    void Awake()
    {
        tr = GetComponent<Transform>();
        ani = GetComponent<SkeletonAnimation>();
        ani.AnimationName = "Idle";
        ani.loop = true;
    }

    void Update()
    {
        randomAction = Random.Range(0, 3);
        dist = Vector2.Distance(tr.position, target);

        if (dist < 0.1f)
        {
            if (randomAction == 0 || randomAction == 1)
            {
                ActionIdle();
            }
            else
            { 
                MakeMovePoint(); 
            }
        }
        else
            Move();

    }
    void ActionIdle()
    {
        startTime= Time.time;
        idleTime = Random.Range(1f, 5f);
        ani.AnimationName = "Idle";
        ani.loop = true;
        //while (true)
        //{
        //    if (Time.time - startTime > idleTime)
        //    {
        //        break;
        //    }
        //}
    }

    private void MakeMovePoint()
    {
        h = Random.Range(-1, 1);
        v = Random.Range(-1, 1);
        target = new Vector3(h - v, h + v);
    }

    private void Move()
    { 
        
        ani.AnimationName = "walk";
        ani.loop = true;
        tr.position = Vector3.Lerp(tr.position, target, Time.deltaTime*0.5f);

    }

    public IEnumerator Reaction()
    {
        int idx=Random.Range(0,11);
        if (idx%2==1)
        {
            ani.AnimationName = "KKamchack";
            ani.loop = false;
            yield return new WaitForSeconds(2f);
        }
        else
        {
            ani.AnimationName = "hello";
            ani.loop = false;
            yield return new WaitForSeconds(2f);
        }
        ani.AnimationName = "Idle";
        ani.loop = true;
    }
}
