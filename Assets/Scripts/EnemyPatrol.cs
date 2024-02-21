using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundDetection;
    public float rayLength = 2f;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // Movement
    private bool _facingRight = true;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(WaitAndChangeDirection());
        _animator.SetBool("Walking", true);
    }

    private IEnumerator WaitAndChangeDirection()
    {
        while (true)
        {
            float horizontalVelocity = (_facingRight ? 1 : -1) * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayLength);
            if (!groundInfo.collider)
            {
                _rigidbody.velocity = Vector2.zero;
                _animator.SetBool("Walking", false);
                yield return new WaitForSeconds(2);

                Flip();
                _animator.SetBool("Walking", true);
            }

            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Flip(); // Invierte la dirección inmediatamente al detectar el jugador
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        // Invierte la escala en x para cambiar la orientación gráfica del enemigo
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
