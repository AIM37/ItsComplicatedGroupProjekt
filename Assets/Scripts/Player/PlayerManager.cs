using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MaxStress = 100;
    [SerializeField][Range(0, MaxStress)] private int stress = 50;
    [SerializeField] private Inventory inventory;

    public void Collect(GameObject item)
    {
        if (item.GetComponent<Collectible>() == null)
        {
            Debug.LogError($"{item} hat kein Script");
            return;
        }
        Collectible script = item.GetComponent<Collectible>();

        if (script.instantUse) script.UseItem(item);
        else inventory.AddItem(item);
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
