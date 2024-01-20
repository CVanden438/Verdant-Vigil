using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Debuff", menuName = "BuffDebuff/Debuff")]
public class DebuffSO : ScriptableObject
{
    public string debuffName;
    public string description;
    public Sprite icon;
}
