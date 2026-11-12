using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Einstellungen")]
    [SerializeField] private int maxWaves = 5;
    [SerializeField] private float waveDuration = 60f;
    [SerializeField] private float stressInterval = 10f;
    [SerializeField] private int stressIncrease = 5;

    [Header("Steigende Schwierigkeit pro Welle (Owner 2 Wave Curve)")]
    [SerializeField] private float extraDurationPerWave = 10f;
    [SerializeField] private int extraStressPerWave = 2;
    [SerializeField] private float extraExSpeedPerWave = 0.4f;
    [SerializeField] private int extraExDamagePerWave = 3;
    [SerializeField] private float extraSpawnDelayPerWave = 0.4f;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text resultText;

    [Header("Andere Systeme")]
    [SerializeField] private ResourceSpawner resourceSpawner;
    [SerializeField] private ExPartnerAI exPartner;

    private int currentWave = 1;
    private float timeLeft;
    private float stressTimer = 0f;
    private bool waveRunning = true;
    private bool lastWaveWon = false;

    private void Start()
    {
        CheckForNullables();
        timeLeft = waveDuration;

        Time.timeScale = 1f;

        resultText.gameObject.SetActive(false);
        waveText.text = currentWave + " / " + maxWaves;

        UpdateTimer();
    }

    private void Update()
    {
        if (!waveRunning)
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (lastWaveWon && currentWave < maxWaves)
                {
                    StartNextWave();
                }
                else
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            return;
        }

        timeLeft -= Time.deltaTime;
        stressTimer += Time.deltaTime;

        if (stressTimer >= stressInterval)
        {
            stressTimer -= stressInterval;
            GameManager.Instance.AddStress(stressIncrease);
        }

        UpdateTimer();

        if (LostWave())
        {
            EndWave(false);
            return;
        }

        if (timeLeft <= 0f)
        {
            EndWave(true);
        }
    }

    private bool LostWave()
    {
        if (GameManager.Instance.GetStress() >= GameManager.Instance.GetMaxStress()) return true;
        if (GameManager.Instance.CheckForSadChild()) return true;

        return false;
    }

    private void StartNextWave()
    {
        currentWave++;

        waveDuration += extraDurationPerWave;
        stressIncrease += extraStressPerWave;
        exPartner.IncreaseDifficulty(extraExSpeedPerWave, extraExDamagePerWave);
        resourceSpawner.IncreaseSpawnTime(extraSpawnDelayPerWave);

        GameManager.Instance.AddStress(-15);

        timeLeft = waveDuration;
        stressTimer = 0f;
        waveRunning = true;

        resultText.gameObject.SetActive(false);
        waveText.text = currentWave + " / " + maxWaves;

        resourceSpawner.enabled = true;
        exPartner.enabled = true;
        Time.timeScale = 1f;

        UpdateTimer();
    }

    void UpdateTimer()
    {
        int seconds = Mathf.CeilToInt(Mathf.Max(0f, timeLeft));
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;

        timerText.text = $"{minutes:00}:{remainingSeconds:00}";
    }

    void EndWave(bool won)
    {
        waveRunning = false;
        lastWaveWon = won;
        resultText.gameObject.SetActive(true);

        if (!won)
        {
            resultText.text = "GAME OVER!\nTOO MUCH STRESS!\n[LEERTASTE] = Neustart"; //rein schreiben dass man auch durch Kinder sterben kann
        }
        else if (currentWave < maxWaves)
            {
                resultText.text = "WAVE " + currentWave + " COMPLETE!\n[LEERTASTE] = Nächste Welle";
            }
            else
            {
                resultText.text = "ALL 5 WAVES COMPLETE!\nGEWONNEN!\n[LEERTASTE] = Neustart";
            }

        resourceSpawner.enabled = false;
        exPartner.enabled = false;

        Time.timeScale = 0f;
    }

    public bool GetWaveRunning()
    {
        return waveRunning;
    }

    private void CheckForNullables()
    {
        if (timerText == null) throw new InvalidOperationException(nameof(timerText) + " fehlt");
        if (waveText == null) throw new InvalidOperationException(nameof(waveText) + " fehlt");
        if (resultText == null) throw new InvalidOperationException(nameof(resultText) + " fehlt");
        if (resourceSpawner == null) throw new InvalidOperationException(nameof(resourceSpawner) + " fehlt");
        if (exPartner == null) throw new InvalidOperationException(nameof(exPartner) + " fehlt");
    }
}