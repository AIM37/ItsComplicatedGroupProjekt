using UnityEngine;
using UnityEngine.InputSystem;

public class ChildObject : MonoBehaviour
{
    private Child child;
    private ChildManager manager;
    private GameObject ePrompt;

    private bool playerInRange;

    public void Init(Child c, ChildManager m, GameObject e)
    {
        child = c;
        manager = m;
        ePrompt = e;
    }

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame) {
            manager.GiveToChild(child);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Hallo");

        playerInRange = true;
        ePrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        ePrompt.SetActive(false);
    }
}
