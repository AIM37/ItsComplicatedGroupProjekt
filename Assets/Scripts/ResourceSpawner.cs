
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [Header("Ressourcen-Prefabs")]
    [SerializeField] private GameObject[] resourcePrefabs;

    [Header("Moegliche Spawn-Positionen")]
    [SerializeField] private Transform spawnParent;
    private List<Transform> spawnPoints = new List<Transform>();

    [Header("Einstellungen")]
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private int maxResources = 12;

    private float timer;

    // Speichert, welche SpawnPoints belegt sind
    private Dictionary<Transform, GameObject> occupiedPoints = new Dictionary<Transform, GameObject>();

    private void Awake()
    {
        foreach (Transform child in spawnParent)
        {
            spawnPoints.Add(child);
        }
    }
    private void Start()
    {
        CheckForNullables();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnResource();
        }
    }

    private void SpawnResource()
    {
        if (resourcePrefabs.Length == 0 ||
            spawnPoints.Count == 0)
            return;

        List<Transform> freePoints = new List<Transform>();

        // Nur freie SpawnPoints sammeln
        foreach (Transform point in spawnPoints)
        {
            if (point == null)
                continue;

            if (!occupiedPoints.ContainsKey(point) ||
                occupiedPoints[point] == null)
            {
                freePoints.Add(point);
            }
        }

        if (freePoints.Count == 0)
            return;

        if (occupiedPoints.Count >= maxResources)
            return;

        // Zufaelligen freien Punkt auswaehlen
        Transform spawnPoint =
            freePoints[UnityEngine.Random.Range(0, freePoints.Count)];

        GameObject prefab =
            resourcePrefabs[UnityEngine.Random.Range(0, resourcePrefabs.Length)];

        GameObject resource = Instantiate(
            prefab,
            spawnPoint.position,
            Quaternion.identity
        );

        occupiedPoints[spawnPoint] = resource;
    }
    private void CheckForNullables()
    {
        if (resourcePrefabs == null) throw new InvalidOperationException(nameof(resourcePrefabs) + " fehlt");
        if (spawnPoints == null) throw new InvalidOperationException(nameof(spawnPoints) + " fehlt");
    }
}
