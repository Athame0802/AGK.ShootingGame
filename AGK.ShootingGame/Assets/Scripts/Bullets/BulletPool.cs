using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour, IBulletPool
{
    [SerializeField] private List<Bullet> bulletPrefabList = new(5);
    private Dictionary<Type, object> bulletPoolDictionary = new(5);

    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private int bulletPoolPreWarmCount = 30;
    [SerializeField] private int bulletPoolCapacity = 100;
    [SerializeField] private int bulletPoolMax = 500;

    private void Awake()
    {
        for (int i = 0; i < bulletPrefabList.Count; i++)
        {
            bulletPrefabList[i].MakeMyTypeBulletPool(this);
        }
    }

    public void MakeBulletPool<T>(T bulletPrefab) where T : Bullet
    {
        ObjectPool<T> bulletPool = new ObjectPool<T>(
            createFunc: () => CreateBullet(bulletPrefab),
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: true,
            defaultCapacity: bulletPoolCapacity,
            maxSize: bulletPoolMax
            );

        bulletPoolDictionary.Add(typeof(T), bulletPool);

        PreWarmPool<T>();
    }

    public void Spawn<T>(Vector3 position, Quaternion rotation) where T : Bullet
    {
        if (!bulletPoolDictionary.TryGetValue(typeof(T), out object poolobject))
        {
            D.LogError($"아직 딕셔너리에 {typeof(T).Name} 풀이 생성되지 않았습니다!");
            return;
        }
        
        ObjectPool<T> pool = (ObjectPool<T>)poolobject;
        Bullet bullet = pool.Get();

        bullet.IsDespawned = false;
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.gameObject.SetActive(true);
    }

    private void PreWarmPool<T>() where T : Bullet
    {
        List<T> tempBullets = new(bulletPoolPreWarmCount);
        int realBulletPoolPreWarmCount = bulletPoolPreWarmCount;

        ObjectPool<T> pool = (ObjectPool<T>)bulletPoolDictionary[typeof(T)];

        for (int i = 0; i < bulletPoolPreWarmCount; i++)
        {
            T bullet = pool.Get();
            
            if (bullet == null)
            {
                D.LogError($"{typeof(T).Name}의 bulletPoolPreWarmCount가 bulletPoolCapacity보다 작음 / bulletPoolPreWarmCount : {bulletPoolPreWarmCount}, bulletPoolCapacity : {bulletPoolCapacity}");
                realBulletPoolPreWarmCount = i;
                break;
            }

            tempBullets.Add(bullet);
        }

        for (int i = 0; i < realBulletPoolPreWarmCount; i++)
        {
            pool.Release(tempBullets[i]);
        }
    }

    private T CreateBullet<T>(T bulletPrefab) where T : Bullet
    {
        T bullet = Instantiate(bulletPrefab);

        ObjectPool<T> pool = (ObjectPool<T>)bulletPoolDictionary[typeof(T)];
        bullet.Construct(this as IBulletPool, (b) => pool.Release(b as T), playerTransform);

        return bullet;
    }

    private void OnTakeFromPool<T>(T bullet) where T : Bullet
    {
        bullet.IsDespawned = false;
    }

    private void OnReturnedToPool<T>(T bullet) where T : Bullet
    {
        bullet.IsDespawned = true;
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject<T>(T bullet) where T : Bullet
    {
        Destroy(bullet.gameObject);
    }
}