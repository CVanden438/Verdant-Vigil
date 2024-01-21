using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    [SerializeField]
    private float _damageAmount;

    [SerializeField]
    private DebuffSO shock;
    public RangedWeaponSO weaponData;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * weaponData.projectileSpeed;
        // Vector3 rotation = transform.position - mousePos;
    }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            return;
        }
        if (collision.GetComponent<EnemyController>())
        {
            // Destroy(collision.gameObject);
            // Destroy(gameObject);
            var healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.TakeDamage(weaponData.damage);
            if (weaponData.debuff)
            {
                collision
                    .GetComponent<BuffDebuffController>()
                    .ApplyDebuff(weaponData.debuff, weaponData.debuffDuration);
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
