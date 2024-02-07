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
    [SerializeField]
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
        //animator.SetBool("Idle", movement == Vector2.zero);
    }

    void FixedUpdate()
    {
        MovePlayer();
        Jump();
        Fall();
        float horizontalVelocity = movement.normalized.x * speed * Time.deltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }


    private void MovePlayer()
    {
        if (!DialogueOnScreen())
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            movement = new Vector2(horizontalInput, 0f);
            animator.SetBool("Idle", false);
            if (isGrounded)
            {
                animator.SetBool("Walking", true);
                 animator.SetBool("Falling", false);
            }
            if (horizontalInput < 0f && facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && facingRight == false)
            {
                Flip();
            }
            else if (horizontalInput == 0f && isGrounded)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Idle", true);
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
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
                animator.SetBool("Walking", false);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("Idle", false);
                animator.SetBool("Jumping", true);
            }
        }
    }

    //Fall
    void Fall()
    {
        // Verifica si el personaje esta cayendo
        if (rb.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
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
