using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;

    [Header("Visual Effects")]
    [SerializeField] private GameObject deathParticles;

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
    }

    private void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        EventManager.TriggerEnemyDefeated(GetInstanceID());
        Destroy(gameObject);
    }
}