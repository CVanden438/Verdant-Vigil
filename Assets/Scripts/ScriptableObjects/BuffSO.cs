using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buffs
{
    enraged,
    speedy
}

[CreateAssetMenu(fileName = "Buff", menuName = "Debuff/Buff")]
public class BuffSO : ScriptableObject
{
    public Buffs buffName;
    public string description;
    public Sprite icon;
}
