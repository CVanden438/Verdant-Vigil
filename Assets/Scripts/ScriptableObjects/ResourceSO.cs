using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resources
{
    gold,
    wood,
    ingots
}

[CreateAssetMenu(fileName = "Resource", menuName = "Items/Resource")]
public class ResourceSO : ScriptableObject
{
    public Resources resourceName;
    public Sprite icon;
}
