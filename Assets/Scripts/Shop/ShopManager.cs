using UnityEngine;

public class ShopManager : MonoBehaviour

{

    private Inventory inventory;
    [SerializeField] private DeliveryManager deliveryManager;

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
                Debug.LogWarning("Liefer-Prefab fehlt!");
                return;
            }

            if (deliveryManager == null)
            {
                Debug.LogWarning("DeliveryManager fehlt!");
                return;
            }

            inventory.RemoveMoney(item.price);
            deliveryManager.Deliver(consumable.deliveryPrefab);
        }
        else if (item is UpgradeItem upgrade)
        {
            Debug.Log("Upgrade ausgewählt: " + upgrade.itemName);
        }
    }

}
