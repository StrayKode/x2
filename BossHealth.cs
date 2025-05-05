using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 150;
    private int currentHealth;

    [Header("References")]
    [SerializeField] private Animator animator; // Animador del jefe

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        animator.SetBool("Dead", true);
        // Lógica adicional para manejar la muerte del jefe
    }
}