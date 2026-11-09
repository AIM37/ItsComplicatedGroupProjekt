using UnityEngine;

public class ChildNeeds : MonoBehaviour
{
    //ChildNumber muss geclean coded werden, damit man später mehrere Kinder hinzufügen kann
    [Header("Welches Kind?")]
    public int childNumber = 1;

    [Header("Beduerfnisse")]
    [SerializeField] private float needInterval = 15f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= needInterval)
        {
            timer = 0f;

            //Mit dem Team besprechen, ob wir solche Zeilenumbrüche lassen.
            GameManager.Instance
                .LoseChildRelationship(childNumber);

            Debug.Log(
                gameObject.name + " braucht Aufmerksamkeit!"
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        bool success = GameManager.Instance.GiveToChild(childNumber);

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
