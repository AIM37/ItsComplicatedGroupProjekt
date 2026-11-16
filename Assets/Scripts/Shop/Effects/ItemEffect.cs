using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
  public abstract void ApplyEffect(GameObject target);
  public virtual void ApplyEffect(GameObject target, int level)
  {
    ApplyEffect(target);
  }
}
