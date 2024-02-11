using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public delegate void CollisionAction(Collider2D collision);
    public event CollisionAction OnCollision;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public WeaponSO weaponData;
    private Vector2 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * weaponData.projectileSpeed;
        // Vector3 rotation = transform.position - mousePos;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(initialPos, transform.position);
        if (distance >= weaponData.rangeRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.GetComponent<PlayerController>())
        {
            return;
        }
        if (collision.GetComponent<EnemyController>())
        {
            // Destroy(collision.gameObject);
            // Destroy(gameObject);
            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(weaponData.rangeDamage);
            OnCollision?.Invoke(collision);
            Destroy(gameObject);
        }
        // Destroy(gameObject);
    }
}
