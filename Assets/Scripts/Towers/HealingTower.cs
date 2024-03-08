using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTower : MonoBehaviour
{
    [SerializeField]
    private HealingTowerSO data;
    private float lastHealTime;
    public GameObject indicator;

    void Start()
    {
        if (TryGetComponent<HealthController>(out var health))
        {
            health._currentHealth = data.maxHealth;
            health._maximumHealth = data.maxHealth;
        }
        lastHealTime = Time.time;
        indicator.transform.localScale = new Vector3(data.aoe, data.aoe);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastHealTime + data.cooldown)
        {
            var colliders = Physics2D.OverlapBoxAll(
                transform.position,
                new Vector2(data.aoe, data.aoe),
                0
            );
            foreach (var collider in colliders)
            {
                if (
                    collider.GetComponent<TowerController>()
                    || collider.GetComponent<WallController>()
                )
                {
                    var health = collider.GetComponent<HealthController>();
                    health.AddHealth(data.healAmount);
                }
            }
            lastHealTime = Time.time;
        }
    }
}
