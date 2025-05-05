using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 3;
    public Animator animator;
    private Grunt grunt;

    void Start()
    {
        currentHealth = Mathf.Max(currentHealth, maxHealth);
        grunt = GetComponent<Grunt>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        StartCoroutine(HurtSequence());

        if (currentHealth == 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(0.2f);
        grunt.enabled = false;
        Destroy(gameObject);
    }

    private IEnumerator HurtSequence()
    {
        animator.SetTrigger("hurt");
        yield return null;
    }
}