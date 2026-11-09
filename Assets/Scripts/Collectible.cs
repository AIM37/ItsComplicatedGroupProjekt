
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum ResourceType
    {
        Heart,
        Money,
        Toy,
        Food
    }

    public ResourceType resourceType;

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
