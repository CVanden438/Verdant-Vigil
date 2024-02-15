using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGround : MonoBehaviour
{
    public int damage;
    public float cd = 1.0f;
    private float lastAttackTime = 0;

    [SerializeField]
    private LayerMask attackableLayer;

    void Update()
    {
        if (Time.time - lastAttackTime >= cd)
        {
            DoDamage();
            lastAttackTime = Time.time;
        }
    }

    void DoDamage()
    {
        Debug.Log("here");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            2.5f,
            attackableLayer
        );
        foreach (var enemy in enemies)
        {
            if (enemy.TryGetComponent<HealthController>(out var health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
