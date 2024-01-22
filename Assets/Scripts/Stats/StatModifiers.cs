using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifiers : MonoBehaviour
{
    [HideInInspector]
    public int Armour { get; set; } = 0;

    [HideInInspector]
    public int MoveSpeedMultiplier { get; set; } = 1;

    [HideInInspector]
    public int DamageTakenModifier { get; set; } = 1;

    private void Start()
    {
        //enemy armour comes from base stats
        if (CompareTag("Enemy"))
        {
            Armour = GetComponent<EnemyController>().data.armour;
        }
    }
}
