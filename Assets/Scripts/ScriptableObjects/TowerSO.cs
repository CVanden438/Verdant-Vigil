using System;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTags
{
    bullet,
    laser,
    slow,
    projectile,
    aoe
}

public enum TowerAttackType
{
    closest,
    highestHP,
    AOE
}

[CreateAssetMenu(fileName = "New Tower", menuName = "Buildings/Tower")]
public class TowerSO : BuildingSO
{
    public int damage;
    public int attackRange;
    public float attackCooldown;
    public int projCount = 1;
    public int maxPierce = 0;
    public int maxChain = 0;
    public DebuffSO debuff;
    public int debuffDuration;
    public GameObject projectilePrefab;
    public TowerSO upgrade;
    public List<TowerSO> maxUpgrades;
    public TowerAttackType attackType;
    public List<TowerTags> tags;
}
