using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]
public class EnemySO : ScriptableObject
{
    public int maxHealth;
    public int damage;
    public int attackCd;
    public int moveSpeed;
}
