using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceCost
{
    public ResourceSO resource;
    public int amount;
}

// [CreateAssetMenu(fileName = "Building", menuName = "Buildings/building")]
public class BuildingSO : ScriptableObject
{
    public string buildingName;
    public string description;
    public int height;
    public int width;
    public Sprite sprite;
    public List<ResourceCost> cost;
    // public int damage;
    // public float attackRange;
    // public float attackCooldown;
    // public GameObject projectilePrefab;
    // public int crystalCost;
    // public int gasCost;
    // public int uraniumCost;
    // public TowerSO upgrade;
    // public List<TowerSO> maxUpgrades;
}
