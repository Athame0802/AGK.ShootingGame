using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLine : MonoBehaviour
{
    [SerializeField] private float BoxSizeX = default;
    [SerializeField] private float BoxSizeY = default;
    
    private Collider2D[] enimies = new Collider2D[5];
    private float boxAngle = 0f;

    private void Update()
    {
        int spawnerCount = 
            Physics2D.OverlapBoxNonAlloc(
                transform.position, 
                new Vector2(BoxSizeX, BoxSizeY), 
                boxAngle, 
                enimies,
                1 << Layers.Enemy
                );

        for (int i = 0; i < spawnerCount; i++)
        {
            if (!enimies[i].TryGetComponent<IEnemy>(out IEnemy enemy))
            {
                return;
            }

            if (!enemy.IsSpawned)
            {
                enemy.IsSpawned = true;
                enemy.IsEnabled = true;
                Scroller.Instance.EnemyCount++;
                D.Log($"적 카운트 : {Scroller.Instance.EnemyCount}");

                if (enemy.IsBoss)
                {
                    Scroller.Instance.BossEnemyCount++;
                    D.Log($"보스 카운트 : {Scroller.Instance.BossEnemyCount}");
                }
            }
        }
    }
}
