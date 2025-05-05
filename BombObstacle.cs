using UnityEngine;

public class BombObstacle : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float countdownTime = 3f;
    [SerializeField] private int damage = 2;

    [Header("Visual Effects")]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Animator animator;

    private bool isCountingDown;

    private void Start()
    {
        isCountingDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCountingDown)
        {
            StartCountdown();
        }
    }

    private void StartCountdown()
    {
        isCountingDown = true;
        animator.SetBool("CountingDown", true);
        Invoke(nameof(Explode), countdownTime);
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}