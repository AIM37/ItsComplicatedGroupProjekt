
using System;
using UnityEngine;

public class ExPartnerAI : MonoBehaviour
{
    // Die drei zustände
    public enum State
    {
        Patrol,
        Chase,
        Pause
    }

    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private int stressDamage = 15;
    [SerializeField] private float damageCooldown = 2f;
    [SerializeField] private GameObject originalPlayer;

    // Zeigt den Zustand in dem Inspktor an
    [SerializeField] private State currentState = State.Patrol;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Vector2 direction;
    private float changeDirectionTime;
    private float pauseEndTime;
    private Color normalColor;

    private void Start()
    {
        CheckForNullables();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = originalPlayer.transform;

        if (spriteRenderer != null)
        {
            normalColor = spriteRenderer.color;
        }

        // behält den Blickkontakt und dreht sich nicht weg
        rb.freezeRotation = true;

        ChooseNewDirection();
    }

    private void FixedUpdate()
    {
        // sucht Spieler ob er in reichweite ist
        Vector2 toPlayer = (Vector2)player.position - rb.position;
        float distance = toPlayer.magnitude;

        bool wallBetween = Physics2D.Linecast(
            rb.position,
            player.position,
            obstacleLayer
        ).collider != null;

        bool canSeePlayer = (distance < detectionRange) && !wallBetween;

        // checkt den state von Ex
        if (currentState == State.Pause)
        {
            // ist er gerade in der pause?
            if (Time.time < pauseEndTime)
            {
                return;
            }
            // wenn pause vorbei ist dann wieder in Patrol
            currentState = State.Patrol;
        }
        else if (canSeePlayer)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }

        // state ausführen
        Vector2 moveDirection = direction;
        float currentSpeed = moveSpeed;

        switch (currentState)
        {
            case State.Patrol:
                if (spriteRenderer != null) spriteRenderer.color = normalColor;

                if (Time.time >= changeDirectionTime)
                {
                    ChooseNewDirection();
                }
                moveDirection = direction;
                break;

            case State.Chase:
                if (spriteRenderer != null) spriteRenderer.color = Color.red;

                moveDirection = toPlayer.normalized;
                currentSpeed = moveSpeed * 1.2f;
                break;
        }

        // schaut ob der body vom ex collidet
        bool blocked = Physics2D.CircleCast(
            rb.position,
            0.35f,
            moveDirection,
            0.6f,
            obstacleLayer
        ).collider != null;

        if (blocked)
        {


            ChooseNewDirection();
            moveDirection = direction;
        }

// bewegt spieler
        rb.MovePosition(
            rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime
        );
    }

    private void ChooseNewDirection()
    {
        // probiert 8 richtungen aus um eine freie zu finden
        for (int i = 0; i < 8; i++)
        {
            float angle = UnityEngine.Random.Range(0f, 360f);
            Vector2 testDir = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            bool isBlocked = Physics2D.CircleCast(
                rb.position,
                0.35f,
                testDir,
                0.8f,
                obstacleLayer
            ).collider != null;

            if (!isBlocked)
            {
                direction = testDir;
                changeDirectionTime = Time.time + 2f;
                return;
            }
        }

        // wenn alle blockiert sind dann einmal umdrehen
        direction = -direction;
        changeDirectionTime = Time.time + 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryCauseStress();
        }
        else
        {
            ChooseNewDirection();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryCauseStress();
        }
    }

    private void TryCauseStress()
    {
        if (currentState == State.Pause)
            return;

        GameManager.Instance.AddStress(stressDamage);

        // Nach einem Treffer geht der ExPartner in den Zustand "Pause" (Grau)
        currentState = State.Pause;
        pauseEndTime = Time.time + damageCooldown;
        if (spriteRenderer != null) spriteRenderer.color = Color.gray;
    }

    // Für Shop-Items (z. B. Zigaretten): versetzt den ExPartner für X Sekunden in Pause
    public void Distract(float seconds)
    {
        currentState = State.Pause;
        pauseEndTime = Time.time + seconds;
        if (spriteRenderer != null) spriteRenderer.color = Color.gray;
    }

    // Für den WaveManager: Macht den ExPartner in jeder neuen Welle etwas stärker!
    public void IncreaseDifficulty(float extraSpeed, int extraDamage)
    {
        moveSpeed += extraSpeed;
        stressDamage += extraDamage;
        currentState = State.Patrol;
        ChooseNewDirection();
    }

    private void CheckForNullables()
    {
        if (originalPlayer == null) throw new InvalidOperationException(nameof(originalPlayer) + " fehlt");
    }
}