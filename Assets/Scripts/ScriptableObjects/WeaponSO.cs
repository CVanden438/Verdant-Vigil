using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapons")]
public class WeaponSO : ItemSO
{
    public int damage;
    public int attackCd;
    public int range;
}
