using UnityEngine;

public class BombScript : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float countdownTime = 3f; // Tiempo de cuenta atrás
    [SerializeField] private int damage = 1; // Daño al jugador
    [SerializeField] private float explosionRadius = 1.5f; // Radio de explosión

    [Header("References")]
    [SerializeField] private Animator animator; // Animador para animaciones de cuenta atrás y explosión
    [SerializeField] private GameObject explosionEffect; // Efecto visual de explosión

    private bool isCountingDown = false;
    private bool isExploding = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCountingDown && collision.CompareTag("Player"))
        {
            StartCountdown();
        }
    }

    public void StartCountdown()
    {
        isCountingDown = true;
        animator.SetBool("CountingDown", true);
        Invoke(nameof(Explode), countdownTime);
    }

    private void Explode()
    {
        if (isExploding) return;

        isExploding = true;

        // Activar animación de explosión
        animator.SetBool("Explode", true);

        // Mostrar efecto visual de explosión
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Aplicar daño a los objetos en el radio de explosión
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }
        }

        // Destruir la bomba después de la animación
        Destroy(gameObject, 0.5f);
    }
}