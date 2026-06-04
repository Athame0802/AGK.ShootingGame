using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public static Scroller Instance = default;

    [SerializeField] private float bossScrollTimeLimit = default;

    public int EnemyCount
    {
        get { return enemyCount; }
        set 
        {
            enemyCount = value;
            D.LogWarning($"enemyCount: {enemyCount}");
            ScrollSpeedCheck();
        }
    }

    public int BossEnemyCount
    {
        get { return bossEnemyCount; }
        set 
        {
            bossEnemyCount = value;
            D.LogWarning($"bossEnemyCount: {bossEnemyCount}");
            ScrollSpeedCheck();
        }
    }

    private int enemyCount = default;
    private int bossEnemyCount = default;

    private float moveSpeed = default;
    private bool isBossScrollTimeAlreadyPassed = default;

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
            if (!isBossScrollTimeAlreadyPassed)
                StartCoroutine(LimitBossScrollTime(bossScrollTimeLimit));
            
            return;
        }

        if (EnemyCount <= 0 && BossEnemyCount <= 0)
        {
            moveSpeed = 1f;
            return;
        }
        
        moveSpeed = 0.75f;
    }

    private IEnumerator LimitBossScrollTime(float bossScrollTimeLimit)
    {
        if (isBossScrollTimeAlreadyPassed)
        {
            yield break;
        }
        
        D.LogWarning("보스 스크롤 타임 제한 실행됨");
        isBossScrollTimeAlreadyPassed = true;

        WaitForSeconds bossScrollTime = new(bossScrollTimeLimit);

        moveSpeed = 0.6f;

        yield return bossScrollTime;

        moveSpeed = 0f;
    }
}