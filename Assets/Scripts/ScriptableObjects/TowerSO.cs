using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Towers/Tower")]
public class TowerSO : ScriptableObject
{
    public string towerName;
    public float damage;
    public float attackRange;
    public float attackCooldown;
    public GameObject projectilePrefab;
    public int crystalCost;
    public int gasCost;
    public int uraniumCost;
}
