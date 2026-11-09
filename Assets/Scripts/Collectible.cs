
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private enum ResourceType
    {
        Heart,
        Money,
        Toy,
        Food
    }

    private ResourceType resourceType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Collect(
                resourceType.ToString()
            );

            Destroy(gameObject);
        }
    }
}
