using UnityEngine;

public class Food : Collectible
{
    public override bool ItemEffect()
    {
        var allChilder = GameManager.Instance.GetAllChildren();
        foreach (Child child in allChilder) //es sollte bestenfalls immer nur ein Kind auf einmal in reichweite sein
        {
            if (child.GivePlayerInRange())
            {
                foreach (ChildNeed need in child.wantedItems)
                {
                    if (need.neededItem.name == this.gameObject.name.Replace("(Clone)", "").Trim())
                    {
                        need.SatisfyNeed();
                        return true;
                    }
                }
            }
        }

        return false;
    }
}