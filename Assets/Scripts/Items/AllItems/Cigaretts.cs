
using UnityEngine;

public class Cigaretts : Collectible
{
    [SerializeField] private float dropDistance = 0.6f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float dropCheckRadius = 0.15f;
    [SerializeField] private GameObject cigarettesTrapPrefab;

    public override bool ItemEffect()
    {
        // Spieler finden
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("Spieler nicht gefunden!");
            return false;
        }

        if (cigarettesTrapPrefab == null)
        {
            Debug.LogWarning("CigarettesTrap-Prefab fehlt!");
            return false;
        }

        // Freien Platz in der Nähe des Spielers suchen
        if (!TryFindDropPosition(player.transform.position,
            out Vector3 dropPosition))
        {
            Debug.Log("Kein freier Platz zum Droppen!");
            return false;
        }

        // Zigaretten-Falle platzieren
        Instantiate(cigarettesTrapPrefab,
            dropPosition, Quaternion.identity);

        return true;
    }

    private bool TryFindDropPosition(
        Vector3 playerPosition, out Vector3 dropPosition)
    {
        Vector2[] directions =
        {
            Vector2.right,
            Vector2.up,
            Vector2.down,
            Vector2.left,
            new Vector2(1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(-1, -1).normalized
        };

        foreach (Vector2 direction in directions)
        {
            Vector2 candidate =
                (Vector2)playerPosition + direction * dropDistance;

            bool positionBlocked = Physics2D.OverlapCircle(
                candidate, dropCheckRadius, obstacleLayer
            ) != null;

            bool pathBlocked = Physics2D.CircleCast(
                playerPosition,
                dropCheckRadius,
                direction,
                dropDistance,
                obstacleLayer
            ).collider != null;

            if (!positionBlocked && !pathBlocked)
            {
                dropPosition = candidate;
                return true;
            }
        }

        dropPosition = playerPosition;
        return false;
    }
}
