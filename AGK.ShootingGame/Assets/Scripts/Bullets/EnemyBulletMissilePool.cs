using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

// 생각 안하고 코딩 날로 먹기 ㅎㅎ;;;;;
public class EnemyBulletMissilePooler : MonoBehaviour, IPoolGetable
{
    public static EnemyBulletMissilePooler Instance = default;

    [SerializeField] private GameObject enemyBulletMissilePrefab = default;
    public ObjectPool<EnemyBulletMissile> EnemyBulletMissilePool = default;
    private const int ENEMY_BULLET_MISSILE_CAPACITY = 50;
    private const int ENEMY_BULLET_MISSILE_MAX = 100;
    private const int ENEMY_BULLET_MISSILE_PRE_WARM_COUNT = 10;

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

        EnemyBulletMissilePool = new ObjectPool<EnemyBulletMissile>(
            createFunc: CreateBullet,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: true,
            defaultCapacity: ENEMY_BULLET_MISSILE_CAPACITY,
            maxSize: ENEMY_BULLET_MISSILE_MAX
            );

        PreWarmPool();
    }

    public void GetAtLocation(Transform location)
    {
        EnemyBulletMissile bullet = EnemyBulletMissilePool.Get();

        bullet.transform.position = location.position;
        bullet.transform.rotation = location.rotation;
    }

    private void PreWarmPool()
    {
        List<EnemyBulletMissile> tempBullets = new(ENEMY_BULLET_MISSILE_PRE_WARM_COUNT);

        for (int i = 0; i < ENEMY_BULLET_MISSILE_PRE_WARM_COUNT; i++)
        {
            tempBullets.Add(EnemyBulletMissilePool.Get());
        }

        for (int i = 0; i < ENEMY_BULLET_MISSILE_PRE_WARM_COUNT; i++)
        {
            EnemyBulletMissilePool.Release(tempBullets[i]);
        }
    }

    private EnemyBulletMissile CreateBullet()
    {
        GameObject obj = Instantiate(enemyBulletMissilePrefab);
        EnemyBulletMissile bullet = obj.GetComponent<EnemyBulletMissile>();
        bullet.SetPool(EnemyBulletMissilePool);

        return bullet;
    }

    private void OnTakeFromPool(EnemyBulletMissile bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.IsDespawned = false;
    }

    private void OnReturnedToPool(EnemyBulletMissile bullet)
    {
        bullet.IsDespawned = true;
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(EnemyBulletMissile bullet)
    {
        Destroy(bullet.gameObject);
    }
}
