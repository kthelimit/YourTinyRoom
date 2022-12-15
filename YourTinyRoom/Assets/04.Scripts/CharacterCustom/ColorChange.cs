using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ColorChange : MonoBehaviour
{
    public Color hairTintColor;
    public Color hairDarkColor;
    public Color pupilTintColor;
    public Color pupilDarkColor;
    public Color clothesTintColor;
    public Color clothesDarkColor;
    MaterialPropertyBlock pupilBlock;
    MaterialPropertyBlock clothesBlock;
    MaterialPropertyBlock hairBlock;
    MeshRenderer meshRenderer;
    int id;
    int idBlack;
    void Start()
    {
        pupilBlock = new MaterialPropertyBlock();
        clothesBlock = new MaterialPropertyBlock();
        hairBlock = new MaterialPropertyBlock();
        meshRenderer = GetComponent<MeshRenderer>();
        id = Shader.PropertyToID("_Color");
        idBlack = Shader.PropertyToID("_Black");

        hairTintColor.a = 1f;
        hairDarkColor.a = 1f;
        pupilTintColor.a = 1f;
        pupilDarkColor.a = 1f;
        clothesTintColor.a = 1f;
        clothesDarkColor.a = 1f;

    }

    public void UpdateColor()
    {
        hairBlock.SetColor(id, hairTintColor);
        hairBlock.SetColor(idBlack, hairDarkColor);
        meshRenderer.SetPropertyBlock(hairBlock, 4);
        pupilBlock.SetColor(id, pupilTintColor);
        pupilBlock.SetColor(idBlack, pupilDarkColor);
        meshRenderer.SetPropertyBlock(pupilBlock, 2);
        clothesBlock.SetColor(id, clothesTintColor);
        clothesBlock.SetColor(idBlack, clothesDarkColor);
        meshRenderer.SetPropertyBlock(clothesBlock, 0);
    }
}
