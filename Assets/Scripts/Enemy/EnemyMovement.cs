using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // public float moveSpeed = 5f; // The speed at which the enemy moves towards the core
    [SerializeField]
    private EnemySO data;

    // private Transform player; // Reference to the player's transform
    public Transform target;
    private Rigidbody2D rb; // Reference to the enemy's rigidbody

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the tag "Player"
        // core = GameObject.FindGameObjectWithTag("Core").transform; // Assuming the core has the tag "Player"
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var speedModifier = GetComponent<StatModifiers>().MoveSpeedModifier.GetFinalValue();
        Vector3 direction = (
            target.position + new Vector3(0.5f, 0.5f) - transform.position
        ).normalized;
        Vector3 movement = data.moveSpeed * speedModifier * direction;
        // rb.MovePosition(transform.position + movement);
        rb.velocity = movement;
        // rb.AddForce(movement);
        // Debug.Log(rb.totalForce);
    }
}
