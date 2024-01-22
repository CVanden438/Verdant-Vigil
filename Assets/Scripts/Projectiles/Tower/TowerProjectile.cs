using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    public int damage;

    private Transform target;
    private Rigidbody2D rb;
    public float force;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = target.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>())
        {
            // Destroy(collision.gameObject);
            // Destroy(gameObject);
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
