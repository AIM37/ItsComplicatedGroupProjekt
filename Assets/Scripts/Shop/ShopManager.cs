using UnityEngine;

public class ShopManager : MonoBehaviour

{

    private Inventory inventory;
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerMovement playerMovement;

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
            if (consumable.applyImmediately)
            {
                if (consumable.effect == null)
                {
                    Debug.LogWarning("Item hat keinen Effekt!");
                    return;
                }
            inventory.RemoveMoney(item.price);
            consumable.effect.ApplyEffect(null);
            Debug.Log(item.itemName + " wurde benutzt!");
            return;
            }

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
            if (upgradeManager == null || playerMovement == null)
            {
                Debug.LogWarning("UpgradeManager oder PlayerMovement fehlt!");
                return;
            }
            
            if (upgrade.effect == null)
            {
                Debug.LogWarning("Upgrade hat keinen Effekt!");
                return;
            }
            
            if (upgradeManager.TryLevelUp(upgrade))
            {
                int level = upgradeManager.GetUpgradeLevel(upgrade);
                inventory.RemoveMoney(upgrade.price);
                upgrade.effect.ApplyEffect(playerMovement.gameObject, level);
                Debug.Log(upgrade.itemName + " gekauft! Level: " + level);
                }
        }


    }

}
