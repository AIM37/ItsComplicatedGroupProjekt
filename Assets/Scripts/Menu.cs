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
        if (menuOpen) 
        {
            GameManager.Instance.PauseGameplay();
        }
        else
        {
            GameManager.Instance.ResumeGameplay();
        }
    }



    public void StartGame()
    {
        SceneManager.LoadScene("Apartment");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("StartGameScene");
    }
}
