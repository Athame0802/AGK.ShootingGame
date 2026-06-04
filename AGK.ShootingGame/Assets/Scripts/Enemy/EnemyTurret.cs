using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class EnemyTurret : MonoBehaviour, IEnemy
{
    [SerializeField] private Bullet bulletPrefab = default;
    [SerializeField] private float attackCooldown = default;
    [SerializeField] private bool isBoss = default;

    public bool IsBoss { get { return isBoss; } private set { isBoss = value; } }
    public bool IsSpawned { get; set; } = false;


    private void Awake()
    {
        IsSpawned = false;
    }

    private void Update()
    {
        if (IsSpawned)
        {
            RotateToPlayer();
        }
    }
   
    public void OnTouchSpawnLine()
    {
        IsSpawned = true;
        StartCoroutine(AttackByCoolDown());
    }

    private void RotateToPlayer()
    {
        Vector3 direction = Bullet.playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnDestroy()
    {
        D.Log("죽음");
        Scroller.Instance.EnemyCount--;

        if (IsBoss)
        {
            Scroller.Instance.BossEnemyCount--;
        }
    }

    private IEnumerator AttackByCoolDown()
    {
        D.Log("적 - 공격 실행");

        WaitForSeconds attackCD = new(attackCooldown);


        while (true)
        {
            bulletPrefab.SpawnAtLocation(transform);

            yield return attackCD;
        }
    }
}

