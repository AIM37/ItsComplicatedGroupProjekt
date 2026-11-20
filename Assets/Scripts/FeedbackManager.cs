using System.Collections;
using TMPro;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager Instance;

    [Header("Platzhalter-UI")]
    [SerializeField] private TMP_Text feedbackText;
    [Tooltip("Wie viele Sekunden der Text sichtbar bleibt")]
    [SerializeField] private float showDuration = 2f;
    [SerializeField] private Color successColor = Color.green;
    [SerializeField] private Color errorColor = Color.red;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [Tooltip("Best채tigungssound bei Erfolg")]
    [SerializeField] private AudioClip confirmSound;
    [Tooltip("Optional: Sound bei Fehlern (z.B. nicht genug Geld)")]
    [SerializeField] private AudioClip errorSound;

    private Coroutine hideRoutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    public static void Show(string message, bool success = true)
    {
        if (Instance != null)
        {
            Instance.ShowFeedback(message, success);
        }
    }

    public void ShowFeedback(string message, bool success = true)
    {
        if (feedbackText == null) return;

        // 1. Text setzen, einf채rben und einblenden
        feedbackText.text = message;
        feedbackText.color = success ? successColor : errorColor;
        feedbackText.gameObject.SetActive(true);

        // 2. Passenden Sound abspielen
        AudioClip clip = success ? confirmSound : errorSound;
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }

        // 3. Laufenden Ausblende-Timer stoppen und neu starten
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
        }
        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSecondsRealtime(showDuration);
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
        hideRoutine = null;
    }

    [ContextMenu("Test Feedback")]
    private void TestFeedback()
    {
        ShowFeedback("Test: Feedback funktioniert!");
    }
}