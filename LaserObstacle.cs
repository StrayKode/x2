using UnityEngine;

public class LaserObstacle : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] private float laserDuration = 3f; // Tiempo que el láser permanece activo
    [SerializeField] private float cooldownDuration = 2f; // Tiempo entre activaciones

    [Header("Visual Effects")]
    [SerializeField] private GameObject laserBody;
    [SerializeField] private GameObject laserBeam;

    private bool isActive;

    private void Start()
    {
        laserBody.SetActive(false);
        laserBeam.SetActive(false);
        StartCoroutine(LaserCycle());
    }

    private IEnumerator LaserCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldownDuration);
            ActivateLaser();

            yield return new WaitForSeconds(laserDuration);
            DeactivateLaser();
        }
    }

    private void ActivateLaser()
    {
        isActive = true;
        laserBody.SetActive(true);
        laserBeam.SetActive(true);
    }

    private void DeactivateLaser()
    {
        isActive = false;
        laserBody.SetActive(false);
        laserBeam.SetActive(false);
    }
}