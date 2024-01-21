using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable")]
public class ConsumableSO : ItemSO
{
    public enum ConsumableType
    {
        food,
        potion
    }
}
