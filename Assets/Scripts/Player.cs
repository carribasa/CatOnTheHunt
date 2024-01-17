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
        if (dialoguePanel == null || !dialoguePanel.activeSelf)
        {
            inputValue = Input.GetAxisRaw("Horizontal");

            switch (inputValue)
            {
                case -1:
                    direction = Vector2.left;
                    FlipSprite(true);
                    break;
                case 0:
                    direction = Vector2.zero;
                    break;
                case 1:
                    direction = Vector2.right;
                    FlipSprite(false);
                    break;
            }

            float targetXPosition = Mathf.Max(transform.position.x + direction.x * moveSpeed * Time.deltaTime, -10.55f);
            transform.position = new Vector2(targetXPosition, transform.position.y);
            isWalking = Mathf.Abs(inputValue) > 0.1f;
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
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }
}
