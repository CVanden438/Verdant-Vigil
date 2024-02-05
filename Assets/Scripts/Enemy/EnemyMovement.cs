using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // public float moveSpeed = 5f; // The speed at which the enemy moves towards the core
    [SerializeField]
    private EnemySO data;

    // private Transform player; // Reference to the player's transform
    private Transform player;
    private Rigidbody2D rb; // Reference to the enemy's rigidbody

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the tag "Player"
        // core = GameObject.FindGameObjectWithTag("Core").transform; // Assuming the core has the tag "Player"
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var speedModifier = GetComponent<StatModifiers>().MoveSpeedMultiplier;
        Vector3 direction = (
            player.position + new Vector3(0.5f, 0.5f) - transform.position
        ).normalized;
        Vector3 movement = data.moveSpeed * direction * speedModifier;
        // rb.MovePosition(transform.position + movement);
        rb.velocity = movement;
        // rb.AddForce(movement);
        // Debug.Log(rb.totalForce);
    }
}
