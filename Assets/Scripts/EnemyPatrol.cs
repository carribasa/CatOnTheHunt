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
    }

    private IEnumerator WaitAndChangeDirection()
{
    while (true) // Bucle infinito para el movimiento continuo
    {
        // Mueve al enemigo en la dirección actual
        float horizontalVelocity = (_facingRight ? 1 : -1) * speed;
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);

        // Lanza un raycast hacia abajo para detectar el suelo
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayLength);
        if (!groundInfo.collider) // Si no detecta suelo
        {
            _rigidbody.velocity = Vector2.zero; // Detiene el movimiento del enemigo
            yield return new WaitForSeconds(3); // Espera 3 segundos

            Flip(); // Cambia de dirección después de esperar
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
