using System;
using System.Collections.Generic;
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

    private List<GameObject> objectPool = new List<GameObject>();

    private float timer;

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
        CreateObjectPool();
    }

    private void CreateObjectPool()
    {
        foreach (GameObject prefab in resourcePrefabs)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject newObj = Instantiate(prefab);
                newObj.name = prefab.name;
                newObj.SetActive(false);
                objectPool.Add(newObj);
            }
        }
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
        if (resourcePrefabs.Length == 0 || spawnPoints.Count == 0)
            return;

        List<Transform> freePoints = new List<Transform>();
        int activeItemsOnFloor = 0;

        foreach (Transform point in spawnPoints)
        {
            if (point == null) continue;

            if (!occupiedPoints.ContainsKey(point) ||
                occupiedPoints[point] == null ||
                !occupiedPoints[point].activeSelf)
            {
                freePoints.Add(point);
            }
            else
            {
                activeItemsOnFloor++;
            }
        }

        if (freePoints.Count == 0 || activeItemsOnFloor >= maxResources)
            return;

        Transform spawnPoint = freePoints[UnityEngine.Random.Range(0, freePoints.Count)];

        GameObject resource = GetFreeItemFromPool();
        if (resource == null) return;

        resource.transform.position = spawnPoint.position;
        resource.SetActive(true);

        occupiedPoints[spawnPoint] = resource;
    }

    private GameObject GetFreeItemFromPool()
    {
        List<GameObject> inactiveItems = new List<GameObject>();

        foreach (GameObject item in objectPool)
        {
            if (!item.activeSelf)
            {
                inactiveItems.Add(item);
            }
        }

        if (inactiveItems.Count == 0)
            return null;

        int randomIndex = UnityEngine.Random.Range(0, inactiveItems.Count);
        return inactiveItems[randomIndex];
    }

    public void IncreaseSpawnTime(float extraSeconds)
    {
        spawnInterval += extraSeconds;
    }

    private void CheckForNullables()
    {
        if (resourcePrefabs == null) throw new InvalidOperationException(nameof(resourcePrefabs) + " fehlt");
        if (spawnPoints == null) throw new InvalidOperationException(nameof(spawnPoints) + " fehlt");
    }
}