
using UnityEngine;

public class CigarettesTrap : MonoBehaviour
{
    [SerializeField] private ItemEffect effect;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || effect == null)
            return;

        ExPartnerAI ex = other.GetComponentInParent<ExPartnerAI>();

        if (ex == null)
            return;

        triggered = true;

        effect.ApplyEffect(ex.gameObject);

        Debug.Log("Ex hat Zigaretten gefunden!");

        Destroy(gameObject);
    }
}
