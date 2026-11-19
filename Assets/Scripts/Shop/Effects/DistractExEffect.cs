
using UnityEngine;

[CreateAssetMenu(fileName = "NewDistractExEffect",
    menuName = "Shop/Effects/Distract Ex")]
public class DistractExEffect : ItemEffect
{
    [SerializeField] private float duration = 10f;

    public override void ApplyEffect(GameObject target)
    {
        if (target == null ||
            !target.TryGetComponent<ExPartnerAI>(out var ex))
        {
            Debug.LogWarning("Kein ExPartnerAI gefunden!");
            return;
        }

        ex.Distract(duration);
    }
}
