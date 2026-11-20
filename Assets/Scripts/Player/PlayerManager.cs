using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MaxStress = 100;
    [SerializeField][Range(0, MaxStress)] private int stress = 50;
    [SerializeField] private Inventory inventory;

    public void Collect(GameObject resource)
    {
        if (resource.GetComponent<Collectible>() == null)
        {
            Debug.LogError($"{resource} hat kein Script");
            return;
        }
        Collectible script = resource.GetComponent<Collectible>();

        if (script.instantUse) script.UseItem();
        else inventory.AddItem(resource);
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
