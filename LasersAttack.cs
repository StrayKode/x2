using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LasersAttack : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] private GameObject laserPrefab; // Prefab del láser
    [SerializeField] private Transform[] laserPoints; // Puntos donde aparecen los lásers
    [SerializeField] private int minLasers = 3; // Mínimo de lásers por ataque
    [SerializeField] private int maxLasers = 6; // Máximo de lásers por ataque
    [SerializeField] private float minDelay = 0.2f; // Retardo mínimo entre lásers
    [SerializeField] private float maxDelay = 0.5f; // Retardo máximo entre lásers

    [Header("References")]
    [SerializeField] private BossAttacksManager bossAttacksManager; // Referencia al sistema del jefe

    private Coroutine currentCoroutine;

    public void StartLaserAttack()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ActivateLasers());
    }

    private IEnumerator ActivateLasers()
    {
        int lasersToSpawn = Random.Range(minLasers, maxLasers + 1);
        List<GameObject> activeLasers = new List<GameObject>();

        for (int i = 0; i < lasersToSpawn; i++)
        {
            if (i >= laserPoints.Length) break;

            GameObject laser = Instantiate(laserPrefab, laserPoints[i].position, Quaternion.identity);
            activeLasers.Add(laser);

            float randomDelay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomDelay);
        }

        yield return new WaitForSeconds(3f); // Tiempo para que los lásers permanezcan activos

        // Destruir los lásers después del ataque
        foreach (GameObject laser in activeLasers)
        {
            if (laser != null)
            {
                Destroy(laser);
            }
        }

        // Notificar al sistema del jefe para continuar con el siguiente ataque
        bossAttacksManager.StartNewAttack();
    }
}