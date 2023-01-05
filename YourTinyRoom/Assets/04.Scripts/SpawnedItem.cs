using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    Renderer renderer;
    private const int IsometricRangePerYUnit = 100;
    private Transform furnitureFolder;
    void Start()
    {
        furnitureFolder = GameObject.Find("FurnitureFolder").transform;
        renderer = GetComponent<Renderer>();
        renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit)+21;
        transform.SetParent(furnitureFolder);
    }
}
