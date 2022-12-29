using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gift", menuName = "New Item/Gift")]
public class GIFT : Item
{
    public enum ItemEffectType
    { 
     LIKE, ENERGY
    };
    public ItemEffectType itemEffectType;
    public float ItemEffectValue;
}
