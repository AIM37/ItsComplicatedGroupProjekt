using UnityEngine;

public class WallManager : MonoBehaviour
{
    private int obstacleLayer;

    private void Awake()
    {
        SetupColliders();
    }

    [ContextMenu("Hitboxen im Editor erzeugen")]
    private void SetupColliders()
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

            if (child.GetComponent<Collider2D>() == null)
            {
                BoxCollider2D box = child.gameObject.AddComponent<BoxCollider2D>();

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