using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float coyoteTimeDuration = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastTimeGrounded;
    private Vector2 dashDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 moveVector = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (isGrounded() || Time.time - lastTimeGrounded <= coyoteTimeDuration)
        {
            if (Time.time - lastTimeGrounded > coyoteTimeDuration)
            {
                lastTimeGrounded = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isDashing)
        {
            dashDirection = new Vector2(horizontal, 0).normalized;
            isDashing = true;
            dashTimeLeft = dashTime;
        }

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = dashDirection * dashSpeed;
                dashTimeLeft -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
            }
        }
        else
        {
            rb.velocity = moveVector;
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            Debug.Log("JUMP");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }
    }

    private bool isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer);
        return groundCheck != null;
        // return true;
    }
}
