using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenMove : MonoBehaviour
{
    Renderer renderer;
    float offset;
    public float speed = 0.2f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = Time.time * speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
    }
}
