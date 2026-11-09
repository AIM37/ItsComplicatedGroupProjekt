
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("Wave 1")]
    public float waveDuration = 60f;
    public float stressInterval = 10f;
    public int stressIncrease = 5;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text waveText;
    public TMP_Text resultText;

    [Header("Andere Systeme")]
    public ResourceSpawner resourceSpawner;
    public ExPartnerAI exPartner;

    private float timeLeft;
    private float stressTimer;
    private bool waveRunning;

    void Start()
    {
        timeLeft = waveDuration;
        stressTimer = 0f;
        waveRunning = true;

        Time.timeScale = 1f;

        if (resultText != null)
            resultText.gameObject.SetActive(false);

        if (waveText != null)
            waveText.text = "1 / 5";

        UpdateTimer();
    }

    void Update()
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
        if (GameManager.Instance.stress >= 100)
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
