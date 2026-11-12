
using UnityEngine;

public class ShopItemButton : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ShopItem item;

    public void BuyItem()
    {
        if (shopManager == null || item == null)
        {
            Debug.LogWarning("ShopManager oder Item fehlt!");
            return;
        }

        shopManager.TryPurchase(item);
    }
}
