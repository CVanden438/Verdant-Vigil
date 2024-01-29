using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Buildings/Tower")]
public class TowerSO : BuildingSO
{
    public int damage;
    public int attackRange;
    public float attackCooldown;
    public GameObject projectilePrefab;
    public TowerSO upgrade;
    public List<TowerSO> maxUpgrades;
}
