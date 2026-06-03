using System.Collections;
using UnityEngine;

public interface IBulletPool
{
    public void Spawn<T>(Vector3 position, Quaternion rotation) where T : Bullet;

    public void MakeBulletPool<T>(T bulletPrefab) where T : Bullet;
}