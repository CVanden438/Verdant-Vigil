using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Buildings/BuffTower")]
public class BuffTowerSO : BuildingSO
{
    public float aoe;
    public float cooldown;
    public int tier = 1;
}
