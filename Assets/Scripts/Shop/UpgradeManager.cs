
using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    private Dictionary<UpgradeItem, int> upgradeLevels =
        new Dictionary<UpgradeItem, int>();

    public int GetUpgradeLevel(UpgradeItem item)
    {
        if (upgradeLevels.TryGetValue(item, out int level))
        {
            return level;
        }

        return 0;
    }

    public bool TryLevelUp(UpgradeItem item)
    {
        if (item == null || item.maxLevel <= 0)
            return false;

        int currentLevel = GetUpgradeLevel(item);

        if (currentLevel >= item.maxLevel)
        {
            Debug.Log("Maximales Upgrade-Level erreicht!");
            return false;
        }

        upgradeLevels[item] = currentLevel + 1;

        Debug.Log(item.itemName + " ist jetzt Level " +
                  upgradeLevels[item]);

        return true;
    }
}
