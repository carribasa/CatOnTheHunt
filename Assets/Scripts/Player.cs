using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private float inputValue;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Animator animator;
    private bool isWalking = false;
    public float jumpForce;
    private bool isGrounded = true;
    public GameObject dialoguePanel;
    public float maxSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();
        UpdateAnimator();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        if (!DialogueOnScreen())
        {
            inputValue = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                switch (inputValue)
                {
                    case -1:
                        rb.AddForce(Vector2.left * moveSpeed);
                        FlipSprite(true);
                        break;
                    case 0:
                        break;
                    case 1:
                        rb.AddForce(Vector2.right * moveSpeed);
                        FlipSprite(false);
                        break;
                }
            }
            isWalking = Mathf.Abs(inputValue) > 0.1f;
            float clampedX = Mathf.Max(rb.position.x, -10.55f);
            rb.position = new Vector2(clampedX, rb.position.y);
        }
    }


    private void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    private void FlipSprite(bool flipX)
    {
        Vector3 scale = transform.localScale;
        scale.x = flipX ? -1f : 1f;
        transform.localScale = scale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Jump()
    {
        if (!DialogueOnScreen())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
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
}
