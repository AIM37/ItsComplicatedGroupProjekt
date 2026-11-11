using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MaxStress = 100;
    [SerializeField][Range(0, MaxStress)] private int stress = 50;
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
                break;
            default:
                inventory.AddItem(resource);
                break;

        }
    }

    public void AddStress(int amount)
    {
        stress = Mathf.Clamp(stress + amount, 0, 100);
    }

    public int GetStress()
    {
        return stress;
    }

    public int GetMaxStress()
    {
        return MaxStress;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
