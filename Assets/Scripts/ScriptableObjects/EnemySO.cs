using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    melee,
    range,
    contact
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxHealth;
    public float damage;
    public float attackCd;
    public float moveSpeed;
    public int armour = 0;
    public EnemyType enemyType;
    public DebuffSO debuff;
    public float debuffDuration;
    public DebuffSO DOT;
    public float DOTDuration;
    public float DOTDamage;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public GameObject prefab;
    public int exp;
    public int difficulty = 0;
    public int coins = 0;
}
