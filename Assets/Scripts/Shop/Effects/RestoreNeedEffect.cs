using UnityEngine;

[CreateAssetMenu(fileName = "NewRestoreNeedEffect", menuName = "Shop/Effects/Restore Need")]
public class RestoreNeedEffect : ItemEffect
{
    [SerializeField] private float restoreAmount = 25f;
    public override void ApplyEffect(GameObject target)
    {
        Debug.Log("KinderEssen wurde benutzt!");
    }
}
