
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
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
    private void Start()
    {
        if (item != null && itemIcon != null)
        {
            itemIcon.sprite = item.icon;
        }
    }
}
