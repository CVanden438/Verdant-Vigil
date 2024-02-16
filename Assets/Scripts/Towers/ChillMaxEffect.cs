using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillMaxEffect : MonoBehaviour, ITowerEffect
{
    [SerializeField]
    private GameObject chilledGround;

    public void CollisionEffect(Vector3 position)
    {
        Instantiate(chilledGround, position, Quaternion.identity);
    }
}
