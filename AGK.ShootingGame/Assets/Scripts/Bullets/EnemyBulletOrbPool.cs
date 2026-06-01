using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

// 생각 안하고 코딩 날로 먹기 ㅎㅎ;;;;;
public class EnemyBulletOrbPooler : MonoBehaviour, IPoolGetable
{
    public static EnemyBulletOrbPooler Instance = default;

    [SerializeField] private GameObject enemyBulletOrbPrefab = default;
    public static ObjectPool<EnemyBulletOrb> EnemyBulletOrbPool = default;
    private const int ENEMY_BULLET_ORB_CAPACITY = 100;
    private const int ENEMY_BULLET_ORB_MAX = 1000;
    private const int ENEMY_BULLET_ORB_PRE_WARM_COUNT = 25;

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

        EnemyBulletOrbPool = new ObjectPool<EnemyBulletOrb>(
            createFunc: CreateBullet,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: true,
            defaultCapacity: ENEMY_BULLET_ORB_CAPACITY,
            maxSize: ENEMY_BULLET_ORB_MAX
            );

        PreWarmPool();
    }

    public void GetAtLocation(Transform location)
    {
        EnemyBulletOrb bullet = EnemyBulletOrbPool.Get();

        bullet.transform.position = location.position;
        bullet.transform.rotation = location.rotation;
    }

    private void PreWarmPool()
    {
        List<EnemyBulletOrb> tempBullets = new(ENEMY_BULLET_ORB_PRE_WARM_COUNT);

        for (int i = 0; i < ENEMY_BULLET_ORB_PRE_WARM_COUNT; i++)
        {
            tempBullets.Add(EnemyBulletOrbPool.Get());
        }

        for (int i = 0; i < ENEMY_BULLET_ORB_PRE_WARM_COUNT; i++)
        {
            EnemyBulletOrbPool.Release(tempBullets[i]);
        }
    }

    private EnemyBulletOrb CreateBullet()
    {
        GameObject obj = Instantiate(enemyBulletOrbPrefab);
        EnemyBulletOrb bullet = obj.GetComponent<EnemyBulletOrb>();
        bullet.SetPool(EnemyBulletOrbPool);

        return bullet;
    }

    private void OnTakeFromPool(EnemyBulletOrb bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.IsDespawned = false;
    }

    private void OnReturnedToPool(EnemyBulletOrb bullet)
    {
        bullet.IsDespawned = true;
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(EnemyBulletOrb bullet)
    {
        Destroy(bullet.gameObject);
    }
}
