using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Collider2D standingCollider;
    private Animator animator;
    private bool isGrounded;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public int maxJumps = 2;
    private int jumpCount;
    public GameObject doubleJumpEffect;
    public bool facingRight = true;
    private float moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = maxJumps;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            jumpCount = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                if (jumpCount == maxJumps - 1)
                {
                    Instantiate(doubleJumpEffect, transform.position, Quaternion.identity);
                }
                jumpCount--;
            }
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = Mathf.Abs(scaler.x) * (facingRight ? 1 : -1);
        transform.localScale = scaler;
    }
}
