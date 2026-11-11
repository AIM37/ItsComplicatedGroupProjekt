using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Child", menuName = "Scriptable Objects/Child")]
public class Child : ScriptableObject
{
    public string childName;
    public Sprite sprite;
    public Vector2 position;
    public List<ChildNeed> wantedItems;

    public void UnofficialUpdate()
    {

        foreach (ChildNeed need in wantedItems)
        {

            need.intervaltimer += Time.deltaTime;
            if (need.intervaltimer >= need.intervalStat)
            {
                need.intervaltimer = 0f;

                need.currentStat = Mathf.Max(0f, need.currentStat - 5f);
            }
            if (need.giveTimer > 0f)
            {
                need.giveTimer -= Time.deltaTime;
            }
        }
    }
}

[Serializable]
public class ChildNeed
{
    public GameObject neededItem;
    [SerializeField][Range(0, 100)] private float defaultCurrentStat = 60f;
    [HideInInspector] [Range(0, 100)] public float currentStat;
    public float intervalStat;
    [HideInInspector] public float intervaltimer = 0f;
    [HideInInspector] public float giveTimer = 0f;

    public void SatisfyNeed()
    {
        giveTimer = 1f;
        currentStat = Mathf.Max(0f, currentStat + 5f);
    }

    public float GiveDefaultCurrentStat()
    {
        return defaultCurrentStat;
    }
}