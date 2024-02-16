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
    public int damage;
    public int attackCd;
    public int moveSpeed;
    public int armour = 0;
    public EnemyType enemyType;
    public DebuffSO debuff;
    public int debuffDuration;
    public DebuffSO DOT;
    public int DOTDuration;
    public int DOTDamage;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public GameObject prefab;
    public int exp;
}
