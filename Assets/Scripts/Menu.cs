using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Apartment");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
