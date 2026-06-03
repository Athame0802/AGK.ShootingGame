using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletMissile : Bullet
{   
    [SerializeField] private float homingTime = 1.25f;
    private float time = default;

    private void OnEnable()
    {
        time = 0f;
        RotateToPlayer();
    }

    private void Update()
    {
        Homing(homingTime, base.damage);
    }

    private void Homing(float homingTime, int damage)
    {
        time += Time.deltaTime;

        if (time <= homingTime)
        {
            RotateToPlayer();
        }

        Move(base.bulletSpeed);
        CheckLayerAndDamage(Layers.Player, damage);
    }

    public override void MakeMyTypeBulletPool(IBulletPool bulletPool)
    {
        base.bulletPool = bulletPool;
        bulletPool.MakeBulletPool<EnemyBulletMissile>(this);
    }

    public override void SpawnAtLocation(Transform transform)
    {
        bulletPool.Spawn<EnemyBulletMissile>(transform.position, transform.rotation);
    }
}
