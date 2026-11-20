using UnityEngine;
using static UnityEditor.Progress;

public class Money : Collectible
{
    public override bool instantUse => true;

    public override bool ItemEffect()
    {
        GameManager.Instance.GetInventory().AddMoney(10);
        return false;
    }
}
