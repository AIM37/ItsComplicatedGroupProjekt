using UnityEngine;

//Sehr fragile Klasse,
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
            GameManager.Instance.Collect(gameObject);

            gameObject.SetActive(false); //Nicht sauber, mach ich später besser
        }
    }
}
