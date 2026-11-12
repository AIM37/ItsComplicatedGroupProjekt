using UnityEngine.InputSystem;
using UnityEngine;

public class PhoneShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;


    private void Start()
    {
        shopPanel.SetActive(false);
    }

    
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleShop();
        }
    }


    private void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
