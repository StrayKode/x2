using UnityEngine;

public class NPCDialogueSystem : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private string[] level1Dialogue;
    [SerializeField] private string[] level2Dialogue;
    [SerializeField] private string[] level3Dialogue;

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMPro.TMP_Text dialogueText;

    private string[] currentDialogue;
    private int currentLineIndex;
    private bool didPlayerInteract;

    private void Start()
    {
        SetDialogueByLevel();
    }

    private void SetDialogueByLevel()
    {
        string currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentLevel == "Level 1")
        {
            currentDialogue = level1Dialogue;
        }
        else if (currentLevel == "Level 2")
        {
            currentDialogue = level2Dialogue;
        }
        else if (currentLevel == "Level 3")
        {
            currentDialogue = level3Dialogue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !didPlayerInteract)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLineIndex < currentDialogue.Length)
        {
            dialogueText.text = currentDialogue[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        didPlayerInteract = true;

        // Otorgar mejora según el nivel
        GrantUpgrade();
    }

    private void GrantUpgrade()
    {
        string currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentLevel == "Level 1")
        {
            playerController.UnlockGun();
        }
        else if (currentLevel == "Level 2")
        {
            playerController.IncreaseJumpForce(1.5f); // Mejora de salto
        }
        else if (currentLevel == "Level 3")
        {
            PlayerPrefs.SetInt("BossDialogue", 1); // Activar estado para el final malo
            PlayerPrefs.Save();
        }
    }
}