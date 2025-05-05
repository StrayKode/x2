using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject goodEndingPanel;
    [SerializeField] private GameObject badEndingPanel;

    [Header("Settings")]
    [SerializeField] private float delayBeforeShow = 2.5f;

    private void OnEnable()
    {
        EventManager.OnGoodEndingTriggered += ShowGoodEnding;
        EventManager.OnBadEndingTriggered += ShowBadEnding;
    }

    private void OnDisable()
    {
        EventManager.OnGoodEndingTriggered -= ShowGoodEnding;
        EventManager.OnBadEndingTriggered -= ShowBadEnding;
    }

    private void ShowGoodEnding()
    {
        StartCoroutine(ShowEndingCoroutine(goodEndingPanel));
    }

    private void ShowBadEnding()
    {
        StartCoroutine(ShowEndingCoroutine(badEndingPanel));
    }

    private System.Collections.IEnumerator ShowEndingCoroutine(GameObject endingPanel)
    {
        yield return new WaitForSeconds(delayBeforeShow);
        endingPanel.SetActive(true);
    }
}