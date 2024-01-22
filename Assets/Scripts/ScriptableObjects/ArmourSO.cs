using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmourSlot
{
    helm,
    chest,
    legs
}

[CreateAssetMenu(fileName = "Armour", menuName = "Items/Armour")]
public class ArmourSO : ItemSO
{
    public ArmourSlot armourSlot;
    public int defence;
}
