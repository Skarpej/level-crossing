using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Collider2D standingCollider;
    private Animator animator;
    private bool isGrounded;
    public float moveSpeed = 5f;
    public float runningSpeedModifier = 1.5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

     public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float crouchSpeedmodifier = 0.5f;
    public int maxJumps = 2;
    private int jumpCount;
    public GameObject doubleJumpEffect;
    public bool facingRight = true;
    public bool isRunning = false;
    private float moveInput;

    public Transform wallCheck;
    public LayerMask wallLayer;
    private bool isTouchingWall;

    private bool crouch = false;
    
    public float wallJumpHorizontalForce = 8f;  // Stronger push-off
    public float wallJumpVerticalForce = 13f;    // Higher launch
    private bool wallJumping;
    public float wallJumpDuration = 0.3f;        // Slightly longer control lock

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = maxJumps;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = isRunning ? moveSpeed * runningSpeedModifier : moveSpeed;
        
        if (!wallJumping)
        {
            rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, wallLayer);

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
            else if (isTouchingWall && !isGrounded)
            {
                PerformWallJump();
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

    private void PerformWallJump()
    {
        wallJumping = true;

        // Determine the direction to push the player away from the wall
        float pushDirection = facingRight ? -1 : 1;

        // Apply force in a diagonal direction away from the wall
        rb.linearVelocity = new Vector2(pushDirection * wallJumpHorizontalForce, wallJumpVerticalForce);

        // Flip the character's direction
        Flip();

        // Lock movement for a short duration to avoid sticking
        Invoke(nameof(StopWallJump), wallJumpDuration);
    }

    private void StopWallJump()
    {
        wallJumping = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = Mathf.Abs(scaler.x) * (facingRight ? 1 : -1);
        transform.localScale = scaler;
    }
}
