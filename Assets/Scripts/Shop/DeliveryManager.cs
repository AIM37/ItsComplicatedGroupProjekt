using UnityEngine;
using System.Collections;

public class DeliveryManager : MonoBehaviour
{   
    [SerializeField] private float deliveryTime = 10f;
    [SerializeField] private Transform deliveryPoint;

    public void Deliver(GameObject itemPrefab)
    {
        StartCoroutine(DeliverAfterDelay(itemPrefab));

        if (itemPrefab == null || deliveryPoint == null)
        {
            Debug.LogWarning("Item-Prefab oder Lieferpunkt fehlt!");
            return;
        }
    }


    private IEnumerator DeliverAfterDelay(GameObject itemPrefab)
    {
        yield return new WaitForSeconds(deliveryTime);

        Instantiate(
            itemPrefab,
            deliveryPoint.position,
            deliveryPoint.rotation
        );
    }

}
