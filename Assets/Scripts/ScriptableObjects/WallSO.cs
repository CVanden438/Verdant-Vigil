using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "Buildings/Wall")]
public class WallSO : BuildingSO
{
    public int maxHealth;
    public int defence;
    public WallSO upgrade;
}
