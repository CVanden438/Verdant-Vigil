using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged Weapon", menuName = "Items/Ranged Weapon")]
public class RangedWeaponSO : WeaponSO
{
    public float projectileSpeed;
    public GameObject projectilePrefab;
}
