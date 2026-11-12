using UnityEngine;

public class WallManager : MonoBehaviour
{
    private int obstacleLayer;

    private void Awake()
    {
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        AddColliders(transform);
    }

    // Geht rekursiv durch alle Kinder und gibt jedem "echten" Objekt einen BoxCollider2D.
    private void AddColliders(Transform root)
    {
        foreach (Transform child in root)
        {
            // Objekte mit Tag "NoCollision" (inkl. aller Kinder) überspringen
            if (child.CompareTag("NoCollision")) continue;

            // Canvas-Objekte sind nur Container: selbst kein Collider, aber ihre Kinder behandeln
            if (child.GetComponent<Canvas>() != null)
            {
                AddColliders(child);
                continue;
            }

            if (child.GetComponent<BoxCollider2D>() == null)
            {
                BoxCollider2D box = child.gameObject.AddComponent<BoxCollider2D>();

                // UI-Objekte: Collider an die Größe des RectTransforms anpassen
                if (child is RectTransform rt)
                {
                    box.size = rt.rect.size;
                    box.offset = rt.rect.center;
                }
            }

            child.gameObject.layer = obstacleLayer;
        }
    }
}
