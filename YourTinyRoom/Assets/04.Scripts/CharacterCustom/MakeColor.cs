using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeColor : MonoBehaviour
{
    public enum ColorPartType { HAIR,PUPIL,CLOTH};
    public ColorPartType colorPart;
    public enum ColorPartType2 { Tint,DARK};
    public ColorPartType2 colorPart2;
    public Image colorShowImage;
    public Text RedColorText;
    public Text GreenColorText;
    public Text BlueColorText;
    float RedColor;
    float GreenColor;
    float BlueColor;
    Color makeColor;
    public InputField hexCode;
    public Slider rSlider;
    public Slider gSlider;
    public Slider bSlider;
    public ColorChange colorChange;

    private void Start()
    {
        makeColor = new Color();
        makeColor.a = 1f;
        Invoke("LoadColor", 0.1f);
    }
    public void LoadColor()
    {
        if (colorPart == ColorPartType.HAIR)
        {
            if (colorPart2 == ColorPartType2.Tint)
            {
                makeColor = colorChange.hairTintColor;
            }

            else
            { 
                makeColor = colorChange.hairDarkColor; 
            }
        }
        else if (colorPart == ColorPartType.PUPIL)
        {
            if (colorPart2 == ColorPartType2.Tint)
            {
                makeColor = colorChange.pupilTintColor;
            }
            else
            {
                makeColor = colorChange.pupilDarkColor;
            }
        }
        else
        {
            if (colorPart2 == ColorPartType2.Tint)
            {
                makeColor = colorChange.clothesTintColor;
            }
            else
            {
                makeColor = colorChange.clothesDarkColor;
            }
        }
        RedColor = 255 * makeColor.r;
        GreenColor = 255 * makeColor.g;
        BlueColor = 255 * makeColor.b;
        colorShowImage.color = makeColor;
        rSlider.value = RedColor;
        gSlider.value = GreenColor;
        bSlider.value = BlueColor;
        RedColorText.text = "R : " + RedColor.ToString();
        GreenColorText.text = "G : " + GreenColor.ToString();
        BlueColorText.text = "B : " + BlueColor.ToString();
        UpdateCharacterColor();
    }

    public void SetRedColor(float sliderValue)
    {
        RedColor = sliderValue;
        RedColorText.text = "R : " + RedColor.ToString();
        ChangingColor();
    }

    public void SetGreenColor(float sliderValue)
    {
        GreenColor = sliderValue;
        GreenColorText.text = "G : " + GreenColor.ToString();
        ChangingColor();
    }

    public void SetBlueColor(float sliderValue)
    {
        BlueColor = sliderValue;
        BlueColorText.text = "B : " + BlueColor.ToString();
        ChangingColor();
    }

    void ChangingColor()
    {
        makeColor.r = RedColor / 255;
        makeColor.g = GreenColor / 255;
        makeColor.b = BlueColor / 255;
        colorShowImage.color = makeColor;
        hexCode.text = "#" + ColorUtility.ToHtmlStringRGB(makeColor);
        UpdateCharacterColor();
    }

    public void HexToColor()
    {
        Color InputColor;
        ColorUtility.TryParseHtmlString(hexCode.text, out InputColor);
        makeColor = InputColor;
        RedColor = 255 * makeColor.r;
        GreenColor = 255 * makeColor.g;
        BlueColor = 255 * makeColor.b;
        colorShowImage.color = makeColor;
        rSlider.value = RedColor;
        gSlider.value = GreenColor;
        bSlider.value = BlueColor;
        RedColorText.text = "R : " + RedColor.ToString();
        GreenColorText.text = "G : " + GreenColor.ToString();
        BlueColorText.text = "B : " + BlueColor.ToString();
        UpdateCharacterColor();
    }

    void UpdateCharacterColor()
    {
        if (colorPart == ColorPartType.HAIR)
        {
            if (colorPart2 == ColorPartType2.Tint)
                colorChange.hairTintColor = makeColor;
            else
                colorChange.hairDarkColor = makeColor;
        }
        else if (colorPart == ColorPartType.PUPIL)
        {
            if (colorPart2 == ColorPartType2.Tint)
                colorChange.pupilTintColor = makeColor;
            else
                colorChange.pupilDarkColor = makeColor;
        }
        else
        {
            if (colorPart2 == ColorPartType2.Tint)
                colorChange.clothesTintColor = makeColor;
            else
                colorChange.clothesDarkColor = makeColor;
        }
                
        colorChange.UpdateColor();
    }

    public void ChangePartTypeH()
    {
        colorPart = ColorPartType.HAIR;
        LoadColor();
    }
    public void ChangePartTypeP()
    {
        colorPart = ColorPartType.PUPIL;
        LoadColor();
    }
    public void ChangePartTypeC()
    {
        colorPart = ColorPartType.CLOTH;
        LoadColor();
    }
}
