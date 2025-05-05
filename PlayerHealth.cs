using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 6;

    private int currentHealth;
    private bool isInvulnerable;

    private Animator animator;

    public bool IsDead() => currentHealth <= 0;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable || IsDead()) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (IsDead())
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    private void Die()
    {
        animator.SetTrigger("death");
        // Trigger game over or respawn logic
    }

    private System.Collections.IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1.5f); // Invulnerability time
        isInvulnerable = false;
    }
}