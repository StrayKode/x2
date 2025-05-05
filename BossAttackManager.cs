using UnityEngine;
using System.Collections;

public class BossAttackManager : MonoBehaviour
{
    [Header("Attack Options")]
    [SerializeField] private BombObstacle bombPrefab;
    [SerializeField] private LaserObstacle laserPrefab;
    [SerializeField] private Transform[] bombSpawnPoints;
    [SerializeField] private Transform[] laserSpawnPoints;

    [Header("Cooldown Settings")]
    [SerializeField] private float attackCooldown = 4f;

    [Header("Health Dependencies")]
    [SerializeField] private BossHealth bossHealth;

    private bool isAttacking;

    private void Start()
    {
        StartCoroutine(AttackCycle());
    }

    private IEnumerator AttackCycle()
    {
        while (bossHealth.CurrentHealth > 0)
        {
            if (!isAttacking)
            {
                StartRandomAttack();
            }

            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private void StartRandomAttack()
    {
        isAttacking = true;

        int randomAttack = Random.Range(0, 2); // 0 = Bomb, 1 = Laser
        switch (randomAttack)
        {
            case 0:
                ExecuteBombAttack();
                break;
            case 1:
                ExecuteLaserAttack();
                break;
        }

        isAttacking = false;
    }

    private void ExecuteBombAttack()
    {
        foreach (Transform spawnPoint in bombSpawnPoints)
        {
            Instantiate(bombPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void ExecuteLaserAttack()
    {
        foreach (Transform spawnPoint in laserSpawnPoints)
        {
            Instantiate(laserPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}