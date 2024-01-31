using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    public GameObject dialoguePanel;

    // Movement
    private Vector2 movement;
    public float speed;
    public float jumpForce;
    private bool facingRight = true;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       
    }

    void LateUpdate()
    {
        animator.SetBool("Idle", movement == Vector2.zero);
    }

    void FixedUpdate()
    {
        MovePlayer();
        Jump();
        float horizontalVelocity = movement.normalized.x * speed * Time.deltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }


    private void MovePlayer()
    {
        if (!DialogueOnScreen())
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            movement = new Vector2(horizontalInput, 0f);

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
            animator.SetBool("Idle", true);
        }
    }

    // Jump
    private void Jump()
    {
        if (!DialogueOnScreen())
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isGrounded = false;
                animator.SetBool("isJumping", true);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    // Is Dialogue on Screen?
    private bool DialogueOnScreen()
    {
        return dialoguePanel != null && dialoguePanel.activeSelf;
    }

    // Flip character
    private void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
