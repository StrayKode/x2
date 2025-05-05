using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueMark;

    [Header("Settings")]
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private float typingSpeed = 0.05f;

    private int currentLineIndex;
    private bool isPlayerInRange;
    private bool isDialogueActive;

    private void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!isDialogueActive)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[currentLineIndex])
            {
                ShowNextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLineIndex];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        currentLineIndex = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        dialogueText.text = string.Empty;

        foreach (char letter in dialogueLines[currentLineIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void ShowNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);
    }
}