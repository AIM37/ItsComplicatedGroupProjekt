
using UnityEngine;

public class ChildNeeds : MonoBehaviour
{
    [Header("Welches Kind?")]
    public int childNumber = 1;

    [Header("Beduerfnisse")]
    public float needInterval = 15f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= needInterval)
        {
            timer = 0f;

            GameManager.Instance
                .LoseChildRelationship(childNumber);

            Debug.Log(
                gameObject.name + " braucht Aufmerksamkeit!"
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        bool success = GameManager.Instance
            .GiveToChild(childNumber);

        if (success)
        {
            Debug.Log(
                gameObject.name + " ist glücklich!"
            );
        }
        else
        {
            Debug.Log(
                "Du hast nicht die passende Ressource!"
            );
        }
    }
}
