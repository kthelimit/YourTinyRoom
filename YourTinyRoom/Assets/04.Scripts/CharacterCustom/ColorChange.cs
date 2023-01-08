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
    public SkeletonMecanim skMecanim;
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

    public void LoadColorData(Color _hairTintColor, Color _hairDarkColor, Color _pupilTintColor, Color _pupilDarkColor, Color _clothesTintColor, Color _clothesDarkColor)
    {
        hairTintColor = _hairTintColor;
        hairDarkColor = _hairDarkColor;
        pupilTintColor = _pupilTintColor;
        pupilDarkColor = _pupilDarkColor;
        clothesTintColor = _clothesTintColor;
        clothesDarkColor = _clothesDarkColor;
    }

    public void RepeatUpdateColor()
    {
        StartCoroutine("UpdateColorRepeat");
    }

    IEnumerator UpdateColorRepeat()
    {
        float startTime = Time.time;
        while (true)
        {
            yield return null;
            UpdateColor();
            if (Time.time - startTime < 0.01f)
                break;
        }
    }

    public void UpdateColor()
    {
        hairBlock.SetColor(id, hairTintColor);
        hairBlock.SetColor(idBlack, hairDarkColor);

        pupilBlock.SetColor(id, pupilTintColor);
        pupilBlock.SetColor(idBlack, pupilDarkColor);

        clothesBlock.SetColor(id, clothesTintColor);
        clothesBlock.SetColor(idBlack, clothesDarkColor);

        if(meshRenderer.materials.Length==14)
        {
            meshRenderer.SetPropertyBlock(hairBlock, 13);
            meshRenderer.SetPropertyBlock(clothesBlock, 1);
            meshRenderer.SetPropertyBlock(clothesBlock, 3);
            meshRenderer.SetPropertyBlock(clothesBlock, 5);
            meshRenderer.SetPropertyBlock(clothesBlock, 7);
            meshRenderer.SetPropertyBlock(clothesBlock, 9);
            meshRenderer.SetPropertyBlock(pupilBlock, 11);
        }
        else if(meshRenderer.materials.Length == 15)
        {
            meshRenderer.SetPropertyBlock(hairBlock, 0);
            meshRenderer.SetPropertyBlock(hairBlock, 14);
            meshRenderer.SetPropertyBlock(clothesBlock, 2);
            meshRenderer.SetPropertyBlock(clothesBlock, 4);
            meshRenderer.SetPropertyBlock(clothesBlock, 6);
            meshRenderer.SetPropertyBlock(clothesBlock, 8);
            meshRenderer.SetPropertyBlock(clothesBlock, 10);
            meshRenderer.SetPropertyBlock(pupilBlock, 12);

        }
        else if(meshRenderer.materials.Length==13)
        {
            meshRenderer.SetPropertyBlock(hairBlock, 12);
            meshRenderer.SetPropertyBlock(clothesBlock, 11);
            meshRenderer.SetPropertyBlock(clothesBlock, 2);
            meshRenderer.SetPropertyBlock(clothesBlock, 4);
            meshRenderer.SetPropertyBlock(clothesBlock, 6);
            meshRenderer.SetPropertyBlock(clothesBlock, 8);
            meshRenderer.SetPropertyBlock(clothesBlock, 10);
        }


    }
}
