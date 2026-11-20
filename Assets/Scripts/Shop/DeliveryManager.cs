using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [Header("Lieferung Einstellungen")]
    [SerializeField] private float deliveryTime = 10f;
    [SerializeField] private Transform deliveryPoint;

    [Header("Anzeige an der Haustür")]
    [Tooltip("Abstand des Countdown-Textes über dem DeliveryPoint (in Welt-Einheiten)")]
    [SerializeField] private Vector3 timerOffset = new Vector3(0f, 0.45f, 0f);
    [Tooltip("Optional: Eigener TMP_Text. Wenn leer, wird automatisch einer an der Tür erzeugt.")]
    [SerializeField] private TMP_Text doorTimerText;

    private class ActiveDelivery
    {
        public GameObject itemPrefab;
        public float remainingTime;
    }

    private readonly List<ActiveDelivery> activeDeliveries = new List<ActiveDelivery>();

    private void Start()
    {
        if (doorTimerText == null && deliveryPoint != null)
        {
            CreateDoorTimerText();
        }

        if (doorTimerText != null)
        {
            doorTimerText.gameObject.SetActive(false);
        }
    }

    private void CreateDoorTimerText()
    {
        GameObject timerObj = new GameObject("DoorDeliveryTimer");
        Vector3 worldPos = deliveryPoint.position + timerOffset;
        worldPos.z = 0f;
        timerObj.transform.position = worldPos;

        TextMeshPro tmp = timerObj.AddComponent<TextMeshPro>();
        tmp.fontSize = 3f;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.yellow;
        tmp.outlineWidth = 0.25f;
        tmp.outlineColor = Color.black;
        tmp.sortingOrder = 6;
        tmp.rectTransform.sizeDelta = new Vector2(5f, 1.2f);

        doorTimerText = tmp;
    }

    public void Deliver(GameObject itemPrefab)
    {
        if (itemPrefab == null || deliveryPoint == null)
        {
            Debug.LogWarning("Item-Prefab oder Lieferpunkt fehlt!");
            return;
        }

        activeDeliveries.Add(new ActiveDelivery
        {
            itemPrefab = itemPrefab,
            remainingTime = deliveryTime
        });
    }

    private void Update()
    {
        if (activeDeliveries.Count == 0)
        {
            if (doorTimerText != null && doorTimerText.gameObject.activeSelf)
            {
                doorTimerText.gameObject.SetActive(false);
            }
            return;
        }

        float maxRemaining = 0f;

        for (int i = activeDeliveries.Count - 1; i >= 0; i--)
        {
            activeDeliveries[i].remainingTime -= Time.deltaTime;

            if (activeDeliveries[i].remainingTime <= 0f)
            {
                SpawnDeliveredItem(activeDeliveries[i].itemPrefab);
                activeDeliveries.RemoveAt(i);
            }
            else if (activeDeliveries[i].remainingTime > maxRemaining)
            {
                maxRemaining = activeDeliveries[i].remainingTime;
            }
        }

        if (activeDeliveries.Count > 0 && doorTimerText != null)
        {
            if (!doorTimerText.gameObject.activeSelf)
            {
                doorTimerText.gameObject.SetActive(true);
            }
            int displaySeconds = Mathf.CeilToInt(maxRemaining);
            doorTimerText.text = $"Lieferung: {displaySeconds}s";
        }
        else if (doorTimerText != null)
        {
            doorTimerText.gameObject.SetActive(false);
        }
    }

    private void SpawnDeliveredItem(GameObject itemPrefab)
    {
        GameObject spawned = Instantiate(
            itemPrefab,
            deliveryPoint.position,
            deliveryPoint.rotation
        );

        StartCoroutine(PopDeliveredItem(spawned.transform));
        FeedbackManager.Show("Lieferung liegt an der Haustür!");
    }

    private IEnumerator PopDeliveredItem(Transform target)
    {
        if (target == null) yield break;

        Vector3 baseScale = target.localScale;
        Vector3 peakScale = baseScale * 1.4f;
        float halfDuration = 0.12f;

        float t = 0f;
        while (t < halfDuration)
        {
            if (target == null) yield break;
            t += Time.deltaTime;
            target.localScale = Vector3.Lerp(baseScale * 0.2f, peakScale, t / halfDuration);
            yield return null;
        }

        t = 0f;
        while (t < halfDuration)
        {
            if (target == null) yield break;
            t += Time.deltaTime;
            target.localScale = Vector3.Lerp(peakScale, baseScale, t / halfDuration);
            yield return null;
        }

        if (target != null)
        {
            target.localScale = baseScale;
        }
    }

    public float GetDeliveryTime()
    {
        return deliveryTime;
    }
}