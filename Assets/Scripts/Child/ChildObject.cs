using UnityEngine;

public class ChildObject : MonoBehaviour
{
    private Child child;
    private ChildManager manager;

    public void Init(Child c, ChildManager m)
    {
        child = c;
        manager = m;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        manager.GiveToChild(child);
    }
}
