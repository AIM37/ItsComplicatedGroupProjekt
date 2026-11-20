using UnityEngine;
public abstract class Collectible : MonoBehaviour
{
    public virtual bool instantUse => false;

    public void UseItem(GameObject item)
    {
        if (ItemEffect()) RemoveItem(item);
    }

    private void RemoveItem(GameObject item) => GameManager.Instance.GetInventory().RemoveItem(item);

    public abstract bool ItemEffect(); //true wenn das Item entfernt werden soll, aber ist nicht perfekte Lösung

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

             GameManager.Instance.Collect(gameObject);
             
             gameObject.SetActive(false); //Nicht sauber, mach ich später besser
        }
    }
}
