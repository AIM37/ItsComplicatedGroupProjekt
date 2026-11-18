using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WorkDesk : MonoBehaviour
{
    [Header("Arbeit Einstellungen")]
    [Tooltip("Wie viele Sekunden man die Taste halten muss, bis der Kreis voll ist")]
    [Min(0.1f)]
    [SerializeField] private float workDuration = 3f;

    [Tooltip("Wie viel Geld man pro vollem Kreis bekommt")]
    [SerializeField] private int moneyReward = 15;

    [Tooltip("Taste, die gehalten werden muss")]
    [SerializeField] private Key workKey = Key.E;

    [Header("UI (Platzhalter)")]
    [Tooltip("Das ganze Anzeige-Objekt über dem Tisch (Canvas mit Kreis + Taste)")]
    [SerializeField] private GameObject promptRoot;

    [Tooltip("Image mit Image Type = Filled, Fill Method = Radial 360")]
    [SerializeField] private Image progressCircle;

    [Tooltip("Text, der die Taste anzeigt (z.B. 'E')")]
    [SerializeField] private TMP_Text keyText;

    private bool playerInRange = false;
    private float progress = 0f;

    private void Start()
    {
        if (keyText != null) keyText.text = workKey.ToString();

        progress = 0f;
        UpdateCircle();
        promptRoot.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;

        bool canWork = GameManager.Instance.GetWaveRunning()
                       && GameManager.Instance.GetMovementAllowed();

        bool keyHeld = Keyboard.current != null && Keyboard.current[workKey].isPressed;

        if (canWork && keyHeld)
        {
            progress += Time.deltaTime;

            if (progress >= workDuration)
            {
                GameManager.Instance.GetInventory().AddMoney(moneyReward);
                progress = 0f;
            }
        }
        else
        {
            progress = 0f;
        }

        UpdateCircle();
    }

    private void UpdateCircle()
    {
        if (progressCircle != null)
        {
            progressCircle.fillAmount = progress / workDuration;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        promptRoot.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        progress = 0f;
        UpdateCircle();
        promptRoot.SetActive(false);
    }
}