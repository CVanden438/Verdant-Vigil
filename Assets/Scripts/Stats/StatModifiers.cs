using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifiers : MonoBehaviour
{
    [HideInInspector]
    public int Armour { get; set; } = 0;

    [HideInInspector]
    public float MoveSpeedMultiplier { get; set; } = 1;

    [HideInInspector]
    public float DamageTakenModifier { get; set; } = 1;

    [HideInInspector]
    public float AttackSpeedModifier { get; set; } = 1;

    [HideInInspector]
    public float DamageOverTime { get; set; } = 0;

    [HideInInspector]
    public float HealthRegen { get; set; } = 0;

    [HideInInspector]
    public float CritChangeAddition { get; set; } = 0;

    [HideInInspector]
    public float CritMultiAddition { get; set; } = 0;

    [HideInInspector]
    public float DamageModifier { get; set; } = 1;

    private void Start()
    {
        //enemy armour comes from base stats
        if (CompareTag("Enemy"))
        {
            Armour = GetComponent<EnemyController>().data.armour;
        }
    }
}
