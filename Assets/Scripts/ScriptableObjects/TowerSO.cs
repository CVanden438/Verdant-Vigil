using System;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTags
{
    bullet,
    laser,
    chill,
    cannon,
    sniper,
    coil,
    projectile,
    aoe
}

public enum TowerAttackType
{
    closest,
    highestHP,
    AOE,
    random
}

[CreateAssetMenu(fileName = "New Tower", menuName = "Buildings/Tower")]
public class TowerSO : BuildingSO
{
    public float damage;
    public float attackRange;
    public float attackCooldown;
    public int projCount = 1;
    public int targetCount = 1;
    public int maxPierce = 0;
    public int maxChain = 0;
    public float aoe = 0;
    public int tier = 1;
    public DebuffSO debuff;
    public float debuffDuration;
    public DebuffSO DOT;
    public float DOTDuration;
    public float DOTDamage;
    public GameObject projectilePrefab;
    public TowerSO upgrade;
    public List<TowerSO> maxUpgrades;
    public TowerAttackType attackType;
    public List<TowerTags> tags;
}
