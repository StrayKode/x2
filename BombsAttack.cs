using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombsAttack : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private GameObject bombPrefab; // Prefab de la bomba
    [SerializeField] private Transform[] bombPoints; // Puntos de aparición de bombas
    [SerializeField] private int minBombs = 3; // Mínimo de bombas
    [SerializeField] private int maxBombs = 6; // Máximo de bombas
    [SerializeField] private float spawnDelay = 0.2f; // Retardo entre apariciones

    [Header("References")]
    [SerializeField] private BossAttacksManager bossAttacksManager; // Referencia al sistema de ataques del jefe

    private Coroutine currentCoroutine;

    public void StartBombsAttack()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ActivateBombs());
    }

    private IEnumerator ActivateBombs()
    {
        int bombsToSpawn = Random.Range(minBombs, maxBombs + 1);
        List<GameObject> spawnedBombs = new List<GameObject>();

        for (int i = 0; i < bombsToSpawn; i++)
        {
            if (i >= bombPoints.Length) break;

            GameObject bomb = Instantiate(bombPrefab, bombPoints[i].position, Quaternion.identity);
            spawnedBombs.Add(bomb);

            yield return new WaitForSeconds(spawnDelay);
        }

        yield return new WaitForSeconds(1.5f);

        // Iniciar cuenta atrás en todas las bombas
        foreach (GameObject bomb in spawnedBombs)
        {
            if (bomb != null)
            {
                BombScript bombScript = bomb.GetComponent<BombScript>();
                if (bombScript != null)
                {
                    bombScript.StartCountdown();
                }
            }
        }

        // Notificar al sistema de ataques del jefe
        yield return new WaitForSeconds(3f);
        bossAttacksManager.StartNewAttack();
    }
}