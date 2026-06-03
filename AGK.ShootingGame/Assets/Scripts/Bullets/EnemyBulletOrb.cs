using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletOrb : Bullet
{
    private void OnEnable()
    {
        RotateToPlayer();
    }
    private void Update()
    {
        Move(bulletSpeed);
        CheckLayerAndDamage(Layers.Player, base.damage);
    }

    public override void MakeMyTypeBulletPool(IBulletPool bulletPool)
    {
        base.bulletPool = bulletPool;
        bulletPool.MakeBulletPool<EnemyBulletOrb>(this);
    }

    public override void SpawnAtLocation(Transform transform)
    {
        bulletPool.Spawn<EnemyBulletOrb>(transform.position, transform.rotation);
    }
}
