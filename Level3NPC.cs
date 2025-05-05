using UnityEngine;

public class Level3NPC : MonoBehaviour
{
    private void OnInteract()
    {
        PlayerPrefs.SetInt("BossDialogue", 1);
        PlayerPrefs.Save();
    }
}