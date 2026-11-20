using UnityEngine;

public class Heart : Collectible
{
    public override bool instantUse => true;
    public override bool ItemEffect()
    {
        GameManager.Instance.AddStress(10);
        return false;
    }
}
