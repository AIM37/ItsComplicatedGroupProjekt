using UnityEngine;

public class Toy : Collectible
{
    public override bool ItemEffect()
    {
        var allChilder = GameManager.Instance.GetAllChildren();
        foreach (Child child in allChilder) //es sollte bestenfalls immer nur ein Kind auf einmal in reichweite sein
        { 
            if (child.GivePlayerInRange())
            {
                //foreach (ChildNeed need in child.allNee)
            }
        }

        return false;
    }
}
