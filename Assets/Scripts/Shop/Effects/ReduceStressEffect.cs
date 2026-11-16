
using UnityEngine;

[CreateAssetMenu(fileName = "NewReduceStressEffect",
    menuName = "Shop/Effects/Reduce Stress")]
    
public class ReduceStressEffect : ItemEffect
{
    [SerializeField] private int stressReduction = 25;

    public override void ApplyEffect(GameObject target)
    {
        GameManager.Instance.AddStress(-stressReduction);

        Debug.Log($"Stress wurde um {stressReduction} reduziert!");
    }
}
