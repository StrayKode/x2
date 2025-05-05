using UnityEngine;
using System.Collections;

public class BossAttacksManager : MonoBehaviour
{
    [Header("Attack Options")]
    [SerializeField] private BombsAttack bombsAttack;
    [SerializeField] private LasersAttack lasersAttack;
    [SerializeField] private BulletBossAttack bulletBossAttack;
    [SerializeField] private EnemySpawnAttack enemySpawnAttack;

    [Header("General Settings")]
    [SerializeField] private float attackCooldown = 4f; // Tiempo entre ataques
    [SerializeField] private BossHealth bossHealth; // Referencia al sistema de salud del jefe
    [SerializeField] private Collider2D detectionCollider; // Collider para detectar al jugador

    private bool isAttacking = false;
    private bool playerDetected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerDetected && bossHealth.CurrentHealth > 0)
        {
            playerDetected = true;
            StartCoroutine(StartAttacks());
        }
    }

    private IEnumerator StartAttacks()
    {
        yield return new WaitForSeconds(2f); // Retardo inicial antes de atacar

        while (bossHealth.CurrentHealth > 0 && playerDetected)
        {
            StartRandomAttack();
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void StartRandomAttack()
    {
        if (isAttacking || bossHealth.CurrentHealth <= 0) return;

        isAttacking = true;

        // Seleccionar un ataque aleatorio
        int randomAttack = Random.Range(1, 5);
        switch (randomAttack)
        {
            case 1:
                bombsAttack.StartBombsAttack();
                break;
            case 2:
                lasersAttack.StartLaserAttack();
                break;
            case 3:
                bulletBossAttack.StartBulletAttack();
                break;
            case 4:
                enemySpawnAttack.StartEnemySpawn();
                break;
        }

        // Reiniciar el estado de ataque después del cooldown
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}