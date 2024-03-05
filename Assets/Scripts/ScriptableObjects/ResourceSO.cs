using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Items/Resource")]
public class ResourceSO : ScriptableObject
{
    public string resourceName;
    public Sprite icon;
}
