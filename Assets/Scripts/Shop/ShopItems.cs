using UnityEngine;


public class ShopItem : ScriptableObject
{
    public string itemName;
    public int price;

    [TextArea(2, 4)]
    public string description;

    public Sprite icon;

    public ItemEffect effect;
}
