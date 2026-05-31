using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public static Scroller Instance = default;
    
    public int enemyCount { get; set; } = default;
    public int bossEnemyCount { get; set; } = default;

    private float moveSpeed = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        Scroll(moveSpeed);
    }

    private void Scroll(float moveSpeed)
    {
        transform.position = new Vector2(
            transform.position.x,
            transform.position.y - moveSpeed * Time.deltaTime);
    }

    public void ScrollSpeedCheck()
    {
        if (enemyCount <= 0 && bossEnemyCount <= 0)
        {
            moveSpeed = 1f;
            return;
        }

        if (bossEnemyCount > 0)
        {
            moveSpeed = 0f;
        }
        
        moveSpeed = 0.75f / enemyCount;
    }
}