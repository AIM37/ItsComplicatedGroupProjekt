
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryDropper : MonoBehaviour
{
    private int selectedIndex = 0;
    [SerializeField] private float dropDistance = 0.6f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float dropCheckRadius = 0.15f;
    [SerializeField] private GameObject cigarettesTrapPrefab;

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (!GameManager.Instance.GetMovementAllowed()
            || !GameManager.Instance.GetWaveRunning())
            return;

        List<GameObject> items =
            GameManager.Instance.GetInventory().GetInventoryList();

        if (items.Count == 0)
            return;

        selectedIndex = Mathf.Clamp(selectedIndex, 0, items.Count - 1);

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            selectedIndex = (selectedIndex + 1) % items.Count;

            Debug.Log("Ausgewählt: " + items[selectedIndex].name);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!IsNearInteraction())
            {
                DropItem(items[selectedIndex]);
            }
        }
    }


    
    private bool IsNearInteraction()
    {
        Collider2D[] nearbyColliders =
            Physics2D.OverlapCircleAll(transform.position, 0.2f);

        foreach (Collider2D collider in nearbyColliders)
        {
            if (collider.GetComponentInParent<ChildObject>() != null ||
                collider.GetComponentInParent<WorkDesk>() != null)
            {
                return true;
           }
        }

        return false;
    }




    
    private void DropItem(GameObject item)
    {
        if (item == null)
            return;

        Inventory inventory = GameManager.Instance.GetInventory();

        // Freien Platz suchen
        if (!TryFindDropPosition(out Vector3 dropPosition))
        {
            Debug.Log("Kein freier Platz zum Droppen!");
            return;
        }

        // Prüfen, ob das Item Zigaretten sind
        bool isCigarettes =
            item.TryGetComponent<Collectible>(out var collectible) &&
            collectible.resourceType == Collectible.ResourceType.Cigarettes;

        if (isCigarettes)
        {
           if (cigarettesTrapPrefab == null)
            {
                Debug.LogWarning("CigarettesTrap-Prefab fehlt!");
                return;
            }

            // Zigaretten-Falle platzieren
            Instantiate(cigarettesTrapPrefab, dropPosition, Quaternion.identity);

           // Zigaretten aus Inventory entfernen
           inventory.RemoveItem(item);
           Destroy(item);

           Debug.Log("Zigaretten-Falle platziert!");
           return;
        }

        // Alle anderen Items normal droppen
        inventory.RemoveItem(item);

        item.transform.position = dropPosition;
        item.SetActive(true);

        Debug.Log("Item gedroppt: " + item.name);
    }

    
    private bool TryFindDropPosition(out Vector3 dropPosition)
    {
        Vector2[] directions =
        {
            Vector2.right,
            Vector2.up,
            Vector2.down,
            Vector2.left,
            new Vector2(1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(-1, -1).normalized
        };

        foreach (Vector2 direction in directions)
        {
            Vector2 candidate =
                (Vector2)transform.position + direction * dropDistance;

            bool positionBlocked = Physics2D.OverlapCircle(
                candidate, dropCheckRadius, obstacleLayer
           ) != null;

            bool pathBlocked = Physics2D.CircleCast(
                transform.position,
                dropCheckRadius,
                direction,
                dropDistance,
                obstacleLayer
            ).collider != null;

           if (!positionBlocked && !pathBlocked)
           {
               dropPosition = candidate;
               return true;
           }
        }

        dropPosition = transform.position;
        return false;
    }


}
