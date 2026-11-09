
using UnityEngine;
using System.Collections.Generic;

public class ResourceSpawner : MonoBehaviour
{
    [Header("Ressourcen-Prefabs")]
    public GameObject[] resourcePrefabs;

    [Header("Moegliche Spawn-Positionen")]
    public Transform[] spawnPoints;

    [Header("Einstellungen")]
    public float spawnInterval = 4f;
    public int maxResources = 12;

    private float timer;

    // Speichert, welche SpawnPoints belegt sind
    private Dictionary<Transform, GameObject> occupiedPoints =
        new Dictionary<Transform, GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnResource();
        }
    }

    void SpawnResource()
    {
        if (resourcePrefabs.Length == 0 ||
            spawnPoints.Length == 0)
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
            freePoints[Random.Range(0, freePoints.Count)];

        GameObject prefab =
            resourcePrefabs[Random.Range(0, resourcePrefabs.Length)];

        GameObject resource = Instantiate(
            prefab,
            spawnPoint.position,
            Quaternion.identity
        );

        occupiedPoints[spawnPoint] = resource;
    }
}
