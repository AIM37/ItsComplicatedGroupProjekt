using UnityEngine;

//Sehr fragile Klasse,
public class Collectible : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

             GameManager.Instance.Collect(gameObject);
             
             gameObject.SetActive(false); //Nicht sauber, mach ich später besser
        }
    }
}
