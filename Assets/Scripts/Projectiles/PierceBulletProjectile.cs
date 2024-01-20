using UnityEngine;

public class PierceBulletProjectile : MonoBehaviour
{
    private float damage;

    [SerializeField]
    private ProjectileController controller;
    private int pierceCount = 0;
    private int maxPierce = 3;

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
            pierceCount += 1;
            if (pierceCount >= maxPierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
