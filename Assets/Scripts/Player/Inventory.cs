using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> inventory = new List<GameObject>();
    private int money;

    public void AddItem(GameObject item)
    {
        inventory.Add(item);
        Debug.Log("Du hast" + item.name);
    }

    public void RemoveItem(GameObject item)
    {
        inventory.Remove(item);
    }

    public List<GameObject> GetInventoryList()
    { 
        return inventory;
    }

    public void AddMoney(int newMoney)
    {
        money += newMoney;
    }

    public int GetMoney()
    {
        return money;
    }
    public void RemoveMoney(int removedMoney)
    {
        money -= removedMoney;
    }
}
