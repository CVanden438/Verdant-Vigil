using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount = 10;

    [SerializeField]
    private float _attackCooldown = 1.0f; // Set the cooldown time in seconds

    private float _lastAttackTime = 0.0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time - _lastAttackTime >= _attackCooldown) // Check if enough time has passed since the last attack
        {
            if (!collision.gameObject.CompareTag("Enemy"))
            {
                if (collision.gameObject.GetComponent<HealthController>())
                {
                    var healthController = collision.gameObject.GetComponent<HealthController>();

                    healthController.TakeDamage(_damageAmount);

                    _lastAttackTime = Time.time; // Update the last attack time
                }
            }
        }
    }
}
