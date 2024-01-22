using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private int damage;
    private Transform target;
    private Rigidbody2D rb;
    private float force = 5;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public int GetDamage()
    {
        return damage;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = target.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
    }
}
