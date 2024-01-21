using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Weapon", menuName = "Items/Melee Weapon")]
public class MeleeWeaponSO : WeaponSO
{
    public enum MeleeType
    {
        stab,
        slash
    }
}
