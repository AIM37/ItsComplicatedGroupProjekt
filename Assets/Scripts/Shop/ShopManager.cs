using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour

{

    private Inventory inventory;
    private Dictionary<UpgradeItem, int> upgradeLevels = new Dictionary<UpgradeItem, int>();

    private void Start()
    {
        inventory = GameManager.Instance.GetInventory();
    }

    public void TryPurchase(ShopItem item)
    {
        if (item == null)
        {
            Debug.LogWarning("Kein Item ausgewählt!");
            return;
        }

        if (inventory.GetMoney() < item.price)
        {
            Debug.Log("Nicht genug Geld!");
            return;
        }

        if (item is ConsumableItem consumable)
        {
            if (consumable.deliveryPrefab == null)
            {
                Debug.LogWarning("Für dieses Item fehlt ein Liefer-Prefab!");
                return;
            }
            Debug.Log("Verbrauchsitem ausgewählt: " + consumable.itemName);
        } 
        else if (item is UpgradeItem upgrade)
        {
            Debug.Log("Upgrade ausgewählt: " + upgrade.itemName);
        }

    }

}
