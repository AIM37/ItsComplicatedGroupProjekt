
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("Wave 1")]
    [SerializeField] private float waveDuration = 60f;
    [SerializeField] private float stressInterval = 10f;
    [SerializeField] private int stressIncrease = 5;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text resultText;

    [Header("Andere Systeme")]
    [SerializeField] private ResourceSpawner resourceSpawner;
    [SerializeField] private ExPartnerAI exPartner;

    private float timeLeft;
    private float stressTimer = 0f;
    private bool waveRunning = true;

    private void Start()
    {
        timeLeft = waveDuration;

        Time.timeScale = 1f;

        if (resultText != null)
            resultText.gameObject.SetActive(false);

        if (waveText != null)
            waveText.text = "1 / 5";

        UpdateTimer();
    }

    private void Update()
    {
        if (!waveRunning)
            return;

        timeLeft -= Time.deltaTime;
        stressTimer += Time.deltaTime;

        // Stress steigt automatisch
        if (stressTimer >= stressInterval)
        {
            stressTimer -= stressInterval;

            GameManager.Instance.AddStress(
                stressIncrease
            );
        }

        UpdateTimer();

        // Zuerst prüfen, ob der Spieler verloren hat
        if (GameManager.Instance.ReturnStress() >= 100)
        {
            EndWave(false);
            return;
        }

        // Zeit abgelaufen = gewonnen
        if (timeLeft <= 0f)
        {
            EndWave(true);
        }
    }

    void UpdateTimer()
    {
        if (timerText == null)
            return;

        int seconds = Mathf.CeilToInt(
            Mathf.Max(0f, timeLeft)
        );

        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;

        timerText.text =
            $"{minutes:00}:{remainingSeconds:00}";
    }

    void EndWave(bool won)
    {
        waveRunning = false;

        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);

            resultText.text = won
                ? "WAVE 1 COMPLETE!"
                : "GAME OVER!\nTOO MUCH STRESS!";
        }

        if (resourceSpawner != null)
            resourceSpawner.enabled = false;

        if (exPartner != null)
            exPartner.enabled = false;

        // Spiel anhalten
        Time.timeScale = 0f;
    }
}
