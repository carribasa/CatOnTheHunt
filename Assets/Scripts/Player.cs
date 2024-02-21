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
    [SerializeField] bool isGrounded;
    [SerializeField] Transform checkGroundedLeft, checkGroundedRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        Jump();
        Fall();
        // CheckGrounded();
        float horizontalVelocity = movement.normalized.x * speed * Time.deltaTime;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }

    // MOVEMENT & ANIMATIONS -------------------------------------------------------------------
    // -----------------------------------------------------------------------------------------

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

    // Is Dialogue on Screen? -> Para para movimiento cuando haya dialogo
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

    // COLLISIONS -------------------------------------------------------------------
    // ------------------------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.5f)
                {
                    isGrounded = true;
                    animator.SetBool("Jumping", false);
                    animator.SetBool("Falling", false);
                    break;
                }
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GameManager.Instance.Lives >= 0)
            {
                Hurt(collision);
                GameManager.Instance.Lives--;
                GameManager.Instance.OnHurt?.Invoke();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Hurt(Collision2D collision)
    {
        animator.SetTrigger("Hurt");

        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(checkGroundedLeft.position, Vector2.down, 0.2f);
        RaycastHit2D hitRight = Physics2D.Raycast(checkGroundedRight.position, Vector2.down, 0.2f);
        isGrounded = hitLeft.collider && hitRight.collider;
    }
}
