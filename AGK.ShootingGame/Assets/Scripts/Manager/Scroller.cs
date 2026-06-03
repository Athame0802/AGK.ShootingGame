using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public static Scroller Instance = default;
    
    public int EnemyCount
    {
        get { return enemyCount; }
        set 
        {
            enemyCount = value;
            ScrollSpeedCheck();
        }
    }

    public int BossEnemyCount
    {
        get { return bossEnemyCount; }
        set 
        {
            bossEnemyCount = value;
            ScrollSpeedCheck();
        }
    }

    private int enemyCount = default;
    private int bossEnemyCount = default;

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

        ScrollSpeedCheck();
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
        if (BossEnemyCount > 0)
        {
            moveSpeed = 0.15f;
            return;
        }

        if (EnemyCount <= 0 && BossEnemyCount <= 0)
        {
            moveSpeed = 1f;
            return;
        }
        
        moveSpeed = 0.75f;
    }
}