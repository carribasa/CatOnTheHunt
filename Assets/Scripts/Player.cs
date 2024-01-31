using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    private float inputValue;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Animator animator;
    public float jumpForce = 6f;
    public GameObject dialoguePanel;
    public float maxSpeed;

    // Movement
    private Vector2 movement;
    private bool facingRight = true;
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void LateUpdate()
    {
        animator.SetBool("Idle", movement == Vector2.zero);
    }

    void FixedUpdate()
    {
            float horizontalVelocity = movement.normalized.x * moveSpeed;
            rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }

    private void MovePlayer()
    {
        if (!DialogueOnScreen())
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            movement = new Vector2(horizontalInput, 0f);

            // Flip character
            if (horizontalInput < 0f && facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && facingRight == false)
            {
                Flip();
            }

            float clampedX = Mathf.Max(rb.position.x, -10.55f);
            rb.position = new Vector2(clampedX, rb.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void Jump()
    {
        if (!DialogueOnScreen())
        {
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
            }
        }
    }

    private bool DialogueOnScreen()
    {
        if (dialoguePanel == null || !dialoguePanel.activeSelf)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
