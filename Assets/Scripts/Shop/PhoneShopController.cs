using UnityEngine.InputSystem;
using UnityEngine;

public class PhoneShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    private bool shopActiv = false;
    
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleShop();
        }
    }


    public void ToggleShop()
    {
        shopActiv = !shopPanel.activeSelf;
        shopPanel.SetActive(shopActiv);
    }

    public bool GetShopActive()
    {
        return shopActiv;
    }
}
