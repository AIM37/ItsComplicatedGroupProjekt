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
            FeedbackManager.Show($"Nicht genug Geld! (kostet {item.price} €)", false); // NEU
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
                FeedbackManager.Show($"{item.itemName} benutzt!");
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

            string arrivalTime = GameManager.Instance.GetClockTimeAfterSeconds(deliveryManager.GetDeliveryTime());
            FeedbackManager.Show($"{item.itemName} bestellt! Ankunft an der Tür um {arrivalTime} Uhr"); // NEU
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
                FeedbackManager.Show($"{upgrade.itemName} gekauft! Level {level}");
            }
            else
            {
                FeedbackManager.Show($"{upgrade.itemName} ist schon auf Max-Level!", false);
            }
        }
    }
}