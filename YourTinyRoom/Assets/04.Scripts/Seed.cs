using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Seed : MonoBehaviour
{
    public GameObject CropPrefab;
    public float SeedPrice = 50f;
    public Text PriceText;

    private void Awake()
    {
        PriceText = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        PriceText.text = SeedPrice.ToString();
    }
    public void ClickCropSeed()
    {
        GridBuildingSystem.gbSystem.InitializeWithBuilding(CropPrefab);
        GameManager.gameManager.DecreaseGold(SeedPrice);
    }
}
