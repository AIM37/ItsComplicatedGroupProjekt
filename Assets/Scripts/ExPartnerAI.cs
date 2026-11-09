
using UnityEngine;

public class ExPartnerAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private int stressDamage = 10;
    [SerializeField] private float damageCooldown = 2f;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 direction;
    private float changeDirectionTime;
    private float nextDamageTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;

        ChooseNewDirection();
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = direction;

        if (player != null)
        {
            Vector2 toPlayer =
                (Vector2)player.position - rb.position;

            float distance = toPlayer.magnitude;

            bool wallBetween = Physics2D.Linecast(
                rb.position,
                player.position,
                obstacleLayer
            ).collider != null;

            if (distance < detectionRange && !wallBetween)
            {
                moveDirection = toPlayer.normalized;
            }
            else if (Time.time >= changeDirectionTime)
            {
                ChooseNewDirection();
                moveDirection = direction;
            }
        }

        // Hindernisse vor der Figur erkennen
        bool blocked = Physics2D.Raycast(
            rb.position,
            moveDirection,
            0.6f,
            obstacleLayer
        ).collider != null;

        if (blocked)
        {
            ChooseNewDirection();
            return;
        }

        rb.MovePosition(
            rb.position +
            moveDirection * moveSpeed * Time.fixedDeltaTime
        );
    }

    private void ChooseNewDirection()
    {
        float angle = Random.Range(0f, 360f);

        direction = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        );

        changeDirectionTime = Time.time + 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryCauseStress(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryCauseStress(collision);
    }

    private void TryCauseStress(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (Time.time < nextDamageTime)
            return;

        GameManager.Instance.AddStress(stressDamage);

        nextDamageTime = Time.time + damageCooldown;
    }
}
