using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#nullable enable

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject? menuScreen;
    private bool menuOpen;

    private void Update()
    {
        if (menuScreen != null)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ToggleMenu();
            }
        }
    }

    public void ToggleMenu()
    {
        menuOpen = !menuScreen.activeSelf;
        menuScreen.SetActive(menuOpen);
    }



    public void StartGame()
    {
        SceneManager.LoadScene("Apartment");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
