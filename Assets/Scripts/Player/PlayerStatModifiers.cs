using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatModifiers : MonoBehaviour
{
    public static PlayerStatModifiers instance;

    private void Awake()
    {
        instance = this;
    }

    [HideInInspector]
    public int armour;

    [HideInInspector]
    public int moveSpeedMultiplier;

    [HideInInspector]
    public int damageTakenModifier;
}
