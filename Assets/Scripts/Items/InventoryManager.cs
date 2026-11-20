
using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour //Muss in Inventory Manager umgeschrieben werden
{
    private int selectedIndex = 0;
    [SerializeField] private GameObject cigarettesTrapPrefab;

    private void Update()
    {
        if (Keyboard.current == null) //Ist dieser Check wirklich nötig?
            return;

        if (!GameManager.Instance.GetMovementAllowed()
            || !GameManager.Instance.GetWaveRunning())
            return;

        List<InventoryEntry> items =
            GameManager.Instance.GetInventory().GetInventoryList();

        if (items.Count == 0)
            return;

        selectedIndex = Mathf.Clamp(selectedIndex, 0, items.Count - 1);

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            selectedIndex = (selectedIndex + 1) % items.Count;

            Debug.Log("Ausgewählt: " + items[selectedIndex].item.name);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!IsNearWorkDesk())
            {
                var currentItem = items[selectedIndex].item;
                currentItem.GetComponent<Collectible>().UseItem(currentItem);
            }
        }
    }


    
    private bool IsNearWorkDesk()
    {
        Collider2D[] nearbyColliders =
            Physics2D.OverlapCircleAll(transform.position, 0.2f);

        foreach (Collider2D collider in nearbyColliders)
        {
            if (collider.GetComponentInParent<WorkDesk>() != null)
            {
                return true;
           }
        }

        return false;
    }
}