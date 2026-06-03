using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBullet : Bullet
{
    private void Update()
    {
        Move(bulletSpeed);
        CheckLayerAndDamage(Layers.Enemy, base.damage);
    }

    public override void MakeMyTypeBulletPool(IBulletPool bulletPool)
    {
        base.bulletPool = bulletPool;
        bulletPool.MakeBulletPool<PlayerBullet>(this);
    }

    public override void SpawnAtLocation(Transform transform)
    {
        bulletPool.Spawn<PlayerBullet>(transform.position, transform.rotation);
    }
}