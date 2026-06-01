using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnLine : MonoBehaviour
{
    [SerializeField] private float boxSizeX = default;
    [SerializeField] private float boxSizeY = default;

    private Collider2D[] despawns = new Collider2D[10];

    private void Update()
    {
        int despawnCount = Physics2D.OverlapBoxNonAlloc(
            transform.position,
            new Vector2(boxSizeX, boxSizeY),
            0f,
            despawns,
            1 << Layers.Enemy | 1 << Layers.EnemyBullet | 1 << Layers.PlayerBullet | 1 << Layers.Item
            );

        for (int i = 0; i < despawnCount; i++)
        {
            if (!despawns[i].TryGetComponent<IDespawnable>(out IDespawnable despawn))
            {
                return;
            }

            despawn.Despawn();
        }
    }
}
