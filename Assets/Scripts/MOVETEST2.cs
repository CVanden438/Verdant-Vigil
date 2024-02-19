using System.Collections;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float dashForce = 20.0f;
    public float coyoteTime = 0.2f;
    public float dashCooldown = 1.0f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isJumping = false;
    private bool isDashing = false;
    private float coyoteTimeCounter;
    private float dashCooldownCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonDown("Dash") && !isDashing && dashCooldownCounter <= 0)
        {
            StartCoroutine(Dash());
        }

        coyoteTimeCounter -= Time.deltaTime;
        dashCooldownCounter -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (isJumping)
        {
            if (rb.IsTouchingLayers(LayerMask.GetMask("Ground")) || coyoteTimeCounter > 0)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                coyoteTimeCounter = 0;
                isJumping = false;
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rb.AddForce(new Vector2(horizontalInput * dashForce, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f); // Dash duration
        isDashing = false;
        dashCooldownCounter = dashCooldown;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            coyoteTimeCounter = coyoteTime;
        }
    }
}
