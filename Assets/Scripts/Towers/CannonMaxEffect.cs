using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CannonMaxEffect : MonoBehaviour, ITowerEffect
{
    [SerializeField]
    private GameObject burningGround;

    public void CollisionEffect(Vector3 position)
    {
        Instantiate(burningGround, position, quaternion.identity);
    }
}
