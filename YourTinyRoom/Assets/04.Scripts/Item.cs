using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType //아이템 유형
    { 
        USED,       //사용가능 아이템
        COLLECT,    //수집용 아이템
        INGREDIENT, //재료용 아이템
        GIFT        //선물용 아이템
    }  
    public string ItemName; //아이템 이름
    public ItemType itemType; //아이템 유형
    public Sprite itemImage;//아이템 이미지(인벤토리에 띄움)
    public GameObject itemPrefab;//아이템 프리팹
    public string ItemDesc; //아이템 설명
    public float ItemPrice; //아이템 가격

}
