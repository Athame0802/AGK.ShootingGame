using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class EnemyTurretMissile : MonoBehaviour, IEnemy
{
    private IPoolGetable pool = default;

    [SerializeField] private float attackCooldown = default;
    [SerializeField] private EnemyHealth enemyHealth = default;
    [SerializeField] private bool isBoss = default;

    private Transform playerTransform = default;

    public bool IsEnabled { set { enabled = value; enemyHealth.enabled = value; } }
    public bool IsBoss { get { return isBoss; } private set { isBoss = value; } }
    public bool IsSpawned { get; set; }

    private void Awake()
    {
        IsEnabled = false;
    }

    private void OnEnable()
    {
        playerTransform = GameObject.FindAnyObjectByType<PlayerAttack>().transform;

        pool = EnemyBulletMissilePooler.Instance as IPoolGetable;
        StartCoroutine(AttackByCoolDown());
    }

    private void Update()
    {
        Vector3 direction = playerTransform.position - transform.position;
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
        D.Log("적 유도 미사일 - 공격 실행");

        WaitForSeconds attackCD = new(attackCooldown);


        while (true)
        {
            pool.GetAtLocation(transform);

            yield return attackCD;
        }
    }
}

