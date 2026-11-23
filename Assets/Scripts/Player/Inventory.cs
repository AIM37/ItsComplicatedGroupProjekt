using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject inventoryBox;
    private List<InventoryEntry> inventory = new();
    private int money;

    public void AddItem(GameObject item)
    {
        var entry = inventory.Find(e => e.item.GetComponent<Collectible>().GetType() == item.GetComponent<Collectible>().GetType());
        if (entry != null)
        {
            entry.count++;
            item.SetActive(false);
        }
        else 
        {
            var newSlot = Instantiate(inventoryBox, inventoryCanvas.transform);
            inventory.Add(new InventoryEntry { item = item, count = 1, slotPlace = newSlot });
        }
        foreach (var e in inventory)
            Debug.Log(e.item.name + " x" + e.count);
    }

    public void RemoveItem(GameObject item)
    {
        var entry = inventory.Find(e => e.item.GetComponent<Collectible>().GetType() == item.GetComponent<Collectible>().GetType());
        if (entry == null) return;

        entry.count--;
        if (entry.count <= 0)
        {
            Destroy(entry.slotPlace);
            inventory.Remove(entry);
        }
    }

    public int GetItemCount(GameObject item)
    {
        var entry = inventory.Find(e => e.item.GetComponent<Collectible>().GetType() == item.GetComponent<Collectible>().GetType());
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
    public GameObject slotPlace;
}