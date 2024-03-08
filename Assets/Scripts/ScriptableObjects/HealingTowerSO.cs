using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Buildings/HealingTower")]
public class HealingTowerSO : BuildingSO
{
    public float aoe;
    public float healAmount;
    public float cooldown;
    public int tier = 1;
}
