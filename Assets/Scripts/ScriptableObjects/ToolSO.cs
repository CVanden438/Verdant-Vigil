using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Items/Tool")]
public class ToolSO : ItemSO
{
    public enum ToolType
    {
        axe,
        pickaxe
    }
}
