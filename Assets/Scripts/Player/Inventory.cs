using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<InventoryEntry> inventory = new();
    private int money;

    public void AddItem(GameObject item)
    {
        var entry = inventory.Find(e => e.item == item);
        if (entry != null) entry.count++;
        else inventory.Add(new InventoryEntry { item = item, count = 1 });
        foreach (var e in inventory)
            Debug.Log(e.item.name + " x" + e.count);
    }

    public void RemoveItem(GameObject item)
    {
        var entry = inventory.Find(e => e.item == item);
        if (entry == null) return;

        entry.count--;
        if (entry.count <= 0) inventory.Remove(entry);
    }

    public int GetItemCount(GameObject item)
    {
        var entry = inventory.Find(e => e.item == item);
        return entry.count;
    }

    public List<InventoryEntry> GetInventoryList()
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

public class InventoryEntry
{
    public GameObject item;
    public int count;
}