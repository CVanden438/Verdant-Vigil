using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType
{
    melee,
    range,
    both
}

public enum MeleeType
{
    stab,
    slash
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
public class WeaponSO : ItemSO
{
    [Header("General")]
    public WeaponType weaponType;

    [Header("Melee")]
    public DebuffSO meleeDebuff;
    public int meleeDebuffDuration;
    public int meleeRange;
    public int meleeDamage;
    public int meleeCooldown;
    public MeleeType meleeType;

    [Header("Range")]
    public DebuffSO rangeDebuff;
    public int rangeDebuffDuration;
    public float projectileSpeed;
    public int rangeRange;
    public int rangeCooldown;
    public int rangeDamage;
    public GameObject projectilePrefab;
}
