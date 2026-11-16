
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedBoostEffect",
    menuName = "Shop/Effects/Speed Boost")]
public class SpeedBoostEffect : ItemEffect
{
    [SerializeField] private float bonusPerLevel = 0.1f;

    public override void ApplyEffect(GameObject target)
    {
        ApplyEffect(target, 1);
    }

    public override void ApplyEffect(GameObject target, int level)
    {
        if (target == null ||
            !target.TryGetComponent<PlayerMovement>(out var movement))
        {
            Debug.LogWarning("PlayerMovement fehlt!");
            return;
        }

        float multiplier = 1f + level * bonusPerLevel;

        movement.SetSpeedMultiplier(multiplier);

        Debug.Log($"SpeedBoost Level {level}: {multiplier}x Geschwindigkeit");
    }
}
