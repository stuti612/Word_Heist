using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform respawnPoint;
    private bool levelComplete = false;
    public GameObject winText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (levelComplete) return;

        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        if (levelComplete) return;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            transform.position = respawnPoint.position;
            rb.linearVelocity = Vector2.zero;
        }

        if (other.CompareTag("Exit"))
        {
            levelComplete = true;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            if (winText != null)
                winText.SetActive(true);
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