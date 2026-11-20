using UnityEngine;
using UnityEngine.InputSystem;

public class ChildObject : MonoBehaviour
{
    private Child child;
    private ChildManager manager;
    private GameObject ePrompt;

    public void Init(Child c, ChildManager m, GameObject e)
    {
        child = c;
        manager = m;
        ePrompt = e;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        child.activePlayerInRange();
        ePrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        child.deactivePlayerInRange();
        ePrompt.SetActive(false);
    }
}
