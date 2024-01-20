using UnityEngine;

public class EnemyContactAttack : MonoBehaviour
{
    // [SerializeField]
    // private float _damageAmount = 10;

    // [SerializeField]
    // private float _attackCooldown = 1.0f; // Set the cooldown time in seconds

    private float _lastAttackTime = 0.0f;

    [SerializeField]
    private EnemySO data;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - _lastAttackTime >= data.attackCd) // Check if enough time has passed since the last attack
            {
                if (collision.gameObject.GetComponent<HealthController>())
                {
                    var healthController = collision.gameObject.GetComponent<HealthController>();

                    healthController.TakeDamage(data.damage);

                    _lastAttackTime = Time.time; // Update the last attack time
                }
            }
        }
    }
}
