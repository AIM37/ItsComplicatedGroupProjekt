using System;
using System.Collections.Generic;
using UnityEngine;

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

                //Mit dem Team besprechen, ob wir solche Zeilenumbrüche lassen.
                need.currentStat = Mathf.Max(0, need.currentStat - 5);

                Debug.Log(childName + " braucht Aufmerksamkeit!");
            }
        }
    }
}

[Serializable]
public class ChildNeed
{
    public GameObject neededItem;
    public float currentStat;
    public float intervalStat;
    public float maxStat;
    public float intervaltimer = 0f;
}