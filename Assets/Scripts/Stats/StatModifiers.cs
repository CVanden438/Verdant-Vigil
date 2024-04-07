using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifiers : MonoBehaviour
{
    public Stat MaxHealthModifier;
    public Stat Armour;
    public Stat MoveSpeedModifier;
    public Stat DamageTakenModifier;
    public Stat AttackSpeedModifier;
    public Stat DamageOverTime;
    public Stat HealthRegen;
    public Stat CritChanceModifier;
    public Stat CritMultiModifier;
    public Stat DamageModifier;

    void Awake()
    {
        MaxHealthModifier = new Stat(1);
        Armour = new Stat(0);
        MoveSpeedModifier = new Stat(1);
        DamageTakenModifier = new Stat(1);
        AttackSpeedModifier = new Stat(1);
        DamageOverTime = new Stat(0);
        HealthRegen = new Stat(0);
        CritChanceModifier = new Stat(0);
        CritMultiModifier = new Stat(0);
        DamageModifier = new Stat(1);
    }

    // public int MaxHealthModifier
    // {
    //     get { return Mathf.RoundToInt(MaxHealthModifierStat.GetFinalValue()); }
    // }
    // public int Armour
    // {
    //     get { return Mathf.RoundToInt(ArmourStat.GetFinalValue()); }
    // }

    // public float MoveSpeedModifier
    // {
    //     get { return MoveSpeedModifierStat.GetFinalValue(); }
    // }

    // public float DamageTakenModifier
    // {
    //     get { return DamageTakenModifierStat.GetFinalValue(); }
    // }

    // public float AttackSpeedModifier
    // {
    //     get { return AttackSpeedModifierStat.GetFinalValue(); }
    // }

    // public float DamageOverTime
    // {
    //     get { return DamageOverTimeStat.GetFinalValue(); }
    // }
    // public float HealthRegen
    // {
    //     get { return HealthRegenStat.GetFinalValue(); }
    // }
    // public float CritChanceModifier
    // {
    //     get { return CritChanceModifierStat.GetFinalValue(); }
    // }
    // public float CritMultiModifier
    // {
    //     get { return CritChanceModifierStat.GetFinalValue(); }
    // }
    // public float DamageModifier
    // {
    //     get { return DamageModifierStat.GetFinalValue(); }
    // }

    private void Start()
    {
        //enemy armour comes from base stats
        if (CompareTag("Enemy"))
        {
            Armour.baseValue = GetComponent<EnemyController>().data.armour;
        }
    }
}
