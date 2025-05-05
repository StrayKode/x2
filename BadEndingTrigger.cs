using UnityEngine;

public class BadEndingTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerPrefs.GetInt("BossDialogue", 0) == 1)
        {
            EventManager.TriggerBadEnding();
        }
    }
}