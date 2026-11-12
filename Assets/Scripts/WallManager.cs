using UnityEngine;

public class WallManager : MonoBehaviour
{
    private int obstacleLayer;

    private void Awake()
    {
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        AddColliders(transform);
    }

    private void AddColliders(Transform root)
    {
        foreach (Transform child in root)
        {
            if (child.CompareTag("NoCollision")) continue;
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
