using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChilledGround : MonoBehaviour
{
    public float cd = 1.0f;
    private float lastAttackTime = 0;
    private float startTime;

    [SerializeField]
    private LayerMask attackableLayer;

    [SerializeField]
    private DebuffSO chill;

    [SerializeField]
    private float chillDuration;

    [SerializeField]
    private float duration;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastAttackTime >= cd)
        {
            ApplyChill();
            lastAttackTime = Time.time;
        }
        if (Time.time - startTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    void ApplyChill()
    {
        Debug.Log("here");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            0.5f,
            attackableLayer
        );
        foreach (var enemy in enemies)
        {
            if (enemy.TryGetComponent<DebuffController>(out var debuffs))
            {
                debuffs.ApplyDebuff(chill, chillDuration);
            }
        }
    }
}
