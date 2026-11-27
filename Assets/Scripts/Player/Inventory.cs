using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

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
            TextChange(entry);
            item.SetActive(false);
        }
        else
        {
            CreateNewSlot(item);
        }
        foreach (var e in inventory)
            Debug.Log(e.item.name + " x" + e.count);
    }

    private void CreateNewSlot(GameObject item)
    {
        var newSlot = Instantiate(inventoryBox, inventoryCanvas.transform);
        var newItem = new InventoryEntry { item = item, count = 1, slotPlace = newSlot };
        inventory.Add(newItem);

        SpriteRenderer inventorySprite = newSlot.transform.Find("Collectable").GetComponent<SpriteRenderer>();
        Debug.Log(item.name);
        Debug.Log(inventorySprite.name);
        inventorySprite.sprite = item.GetComponentInChildren<SpriteRenderer>().sprite;

        TextChange(newItem);
    }

    private void TextChange(InventoryEntry entry)
    {
        TMP_Text count = entry.slotPlace.transform.Find("Count").GetComponent<TMP_Text>();
        count.text = ($"{entry.count}x");
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