using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform respawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // Debug.Log(isGrounded);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void LateUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    // private void OnDrawGizmosSelected()
    // {
    //     if (groundCheck == null) return;
    //     Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    // }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            transform.position = respawnPoint.position;
            rb.linearVelocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Guard"))
        {
            transform.position = respawnPoint.position;
            rb.linearVelocity = Vector2.zero;
        }
    }
}