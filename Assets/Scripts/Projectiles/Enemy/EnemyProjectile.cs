using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public delegate void CollisionAction(Collider2D collision);
    public event CollisionAction OnCollision;

    // private Rigidbody2D rb;
    public GameObject target;
    private float speed = 5;
    public EnemySO data { get; set; }
    public Vector3 Direction { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has the tag "Player"
        Direction = target.transform.position - transform.position;
        Direction = Direction.normalized;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // rb = GetComponent<Rigidbody2D>();
        transform.position += speed * Time.deltaTime * Direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            var health = collision.gameObject.GetComponent<HealthController>();
            health.TakeDamage(data.damage);
            OnCollision?.Invoke(collision);
            Destroy(gameObject);
        }
    }
}
