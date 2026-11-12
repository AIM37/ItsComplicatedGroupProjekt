using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    [SerializeField] private List<Child> childern;

    private void Start()
    {
        
        foreach(Child child in childern)
        {
            foreach (ChildNeed need in child.wantedItems)
            {
                need.currentStat = need.GiveDefaultCurrentStat();
            }
            var newChild = new GameObject(child.name);
            var sortingLayer = newChild.AddComponent<SpriteRenderer>();
            sortingLayer.sprite = child.sprite;
            sortingLayer.sortingOrder = 2;
            newChild.AddComponent<CircleCollider2D>().isTrigger = true;
            newChild.AddComponent<ChildObject>().Init(child, this);
            newChild.transform.SetParent(transform, false);
            newChild.transform.localPosition = child.position;
            newChild.transform.localScale = Vector3.one * 0.1f;
        }
    }
    private void Update()
    {
        if (GameManager.Instance.GetWaveRunning())
        {
            foreach (var child in childern)
            {
                child.UnofficialUpdate();
            }
        }
    }

    public bool CheckForSadChild()
    {
        foreach(Child child in childern)
        {
            foreach(ChildNeed need in child.wantedItems)
            {
                if (need.currentStat <= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public List<Child> GetChildList()
    {
        return childern;
    }

    public void GiveToChild(Child child)
    {
        foreach (Child realChild in childern)
        {
            if (realChild == child)
            {
                var playerInventory = GameManager.Instance.GetInventory().GetInventoryList(); //Maybe einen Weg überlegen wie das etwas kürzer geht
                foreach (ChildNeed need in realChild.wantedItems)
                {
                    foreach (var item in playerInventory)
                    {
                        if (need.neededItem.name == item.name.Replace("(Clone)", "").Trim()) //Das später schöner mit einer ID oder so machen
                        {
                            need.SatisfyNeed();
                            GameManager.Instance.GetInventory().RemoveItem(item);
                            break;
                        }
                    }
                }
            }
        }
    }
}
