using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Debuffs
{
    shock,
    electrocute,
    chill,
    freeze,
    bleed,
    burn,
    stun
}

[CreateAssetMenu(fileName = "Debuff", menuName = "Debuff/Debuff")]
public class DebuffSO : ScriptableObject
{
    public Debuffs debuffName;
    public string description;
    public Sprite icon;
}
