using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public Transform target;
    public float xCorrection=-0.2f;
    public float yCorrection=1.7f;
    private Transform camTr;

    private void Start()
    {
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        if (target.eulerAngles.y == 180f)
        {
            xCorrection = 0.2f;
        }
        else
        {
            xCorrection = -0.2f;
        }
        camTr.position = new Vector3(target.position.x +xCorrection, target.position.y + yCorrection, -3.5f);

    }
}
