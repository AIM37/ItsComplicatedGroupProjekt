using UnityEngine;
public abstract class Collectible : MonoBehaviour
{
    public virtual bool instantUse => false;

    public abstract void UseItem();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

             GameManager.Instance.Collect(gameObject);
             
             gameObject.SetActive(false); //Nicht sauber, mach ich später besser
        }
    }
}
