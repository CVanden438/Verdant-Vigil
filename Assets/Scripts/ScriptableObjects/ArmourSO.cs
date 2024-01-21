using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armour", menuName = "Items/Armour")]
public class ArmourSO : ItemSO
{
    public enum Slot
    {
        helm,
        chest,
        legs
    }

    public int defence;
}
