using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField][Range(0, 100)] private int stress = 50;
    [SerializeField] private Inventory inventory;

    public void Collect(GameObject resource)
    {
        switch (resource.tag)
        {
            case "Money":
                inventory.AddMoney(10);
                break;
            case "Health":
                AddStress(-10);
                Debug.Log("Stress ist " + stress);
                break;
            default:
                inventory.AddItem(resource);
                break;

        }
    }

    public void AddStress(int amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, 100);
        Debug.Log("Momentaner Stres:" + stress);
    }

    public int GetStress()
    {
        return stress;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
