﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType //아이템 유형
    { 
        USED,       //소비 아이템
        COLLECT,    //수집용 아이템
        INGREDIENT, //재료용 아이템
        CUSTOM,     //커스텀용 아이템
        FURNITURE,   //가구용 아이템
        GIFT        //선물용 아이템
    }  
    public enum ItemPriceType //아이템 구매자원 유형
    {
        GOLD,
        CRYSTAL
    }
    public string ItemName; //아이템 이름
    public ItemType itemType; //아이템 유형
    public Sprite itemImage;//아이템 이미지(인벤토리에 띄움)
    public string ItemDesc; //아이템 설명
    public float ItemPrice; //아이템 가격
    public ItemPriceType itemPriceType;//아이템 구매자원 유형
    public int ItemNumber;
    //추후 CSV랑 순서 맞추기

}
