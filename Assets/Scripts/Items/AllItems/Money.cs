using UnityEngine;

public class Money : Collectible
{
    public override bool instantUse => true;

    public override void UseItem()
    {
        GameManager.Instance.GetInventory().AddMoney(10);
    }
}
