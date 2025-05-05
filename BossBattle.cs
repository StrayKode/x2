using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject tileMap; // Tilemap que se activa/desactiva
    [SerializeField] private BossHealth bossHealth; // Salud del jefe
    [SerializeField] private BossAttacksManager bossAttacksManager; // Sistema de ataques del jefe

    private void Start()
    {
        tileMap.SetActive(false); // Inicialmente desactivado
    }

    private void Update()
    {
        if (bossHealth.CurrentHealth <= 0)
        {
            tileMap.SetActive(false); // Desactivar tilemap si el jefe muere
        }
        else if (bossAttacksManager.PlayerDetected)
        {
            tileMap.SetActive(true); // Activar tilemap cuando el jugador es detectado
        }
    }
}