using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTower : MonoBehaviour
{
    [SerializeField]
    private BuffTowerSO data;
    public float damageBuffAmount = 1.2f;

    private float lastBuffTime;

    // public GameObject indicator;
    void Start()
    {
        if (TryGetComponent<HealthController>(out var health))
        {
            health._currentHealth = data.maxHealth;
            health._maximumHealth = data.maxHealth;
        }
        lastBuffTime = Time.time;
        ApplyBuff();
        // indicator.transform.localScale = new Vector3(data.aoe, data.aoe);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastBuffTime + data.cooldown)
        {
            ApplyBuff();
        }
    }

    void ApplyBuff()
    {
        var colliders = Physics2D.OverlapBoxAll(
            transform.position,
            new Vector2(data.aoe, data.aoe),
            0
        );
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<TowerController>() || collider.GetComponent<WallController>())
            {
                if (collider.TryGetComponent<StatModifiers>(out var stats))
                {
                    stats.DamageModifier = damageBuffAmount;
                }
                if (collider.TryGetComponent<SpriteRenderer>(out var sprite))
                {
                    sprite.color = Color.blue;
                }
            }
        }
        lastBuffTime = Time.time;
    }
}
