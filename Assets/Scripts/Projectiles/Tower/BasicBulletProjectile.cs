using UnityEngine;

public class BasicBulletProjectile : MonoBehaviour
{
    private float damage;

    [SerializeField]
    private ProjectileController controller;

    void Start()
    {
        damage = controller.GetDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>())
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
