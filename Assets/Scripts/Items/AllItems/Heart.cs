using UnityEngine;

public class Heart : Collectible
{
    public override bool instantUse => true;
    public override void UseItem()
    {
        GameManager.Instance.AddStress(10);
    }
}
