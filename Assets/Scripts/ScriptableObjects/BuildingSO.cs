using System;
using System.Collections.Generic;
using UnityEngine;

// public enum Resources
// {
//     coins,
//     ingots,
//     gems
// }

[Serializable]
public class ResourceCost
{
    public ResourceSO resourceName;
    public int resourceAmount;
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
    public GameObject prefab;
    public int maxHealth;
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
