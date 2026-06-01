using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletPool : MonoBehaviour, IPoolGetable
{
    [SerializeField] private GameObject playerBulletPrefab = default;
    private ObjectPool<PlayerBullet> playerBulletPool = default;
    private const int PLAYER_BULLET_CAPACITY = 100;
    private const int PLAYER_BULLET_MAX = 500;
    private const int PLAYER_BULLET_PRE_WARM_COUNT = 30;

    private void Awake()
    {
        playerBulletPool = new ObjectPool<PlayerBullet>(
            createFunc : CreateBullet, 
            actionOnGet : OnTakeFromPool,
            actionOnRelease : OnReturnedToPool,
            actionOnDestroy : OnDestroyPoolObject,
            collectionCheck : true,
            defaultCapacity : PLAYER_BULLET_CAPACITY,
            maxSize : PLAYER_BULLET_MAX
            );
    
        PreWarmPool();
    }

    public void GetAtLocation(Transform location)
    {
        PlayerBullet bullet = playerBulletPool.Get();

        bullet.transform.position = location.position;
        bullet.transform.rotation = location.rotation;
    }

    private void PreWarmPool()
    {
        List<PlayerBullet> tempBullets = new(PLAYER_BULLET_PRE_WARM_COUNT);

        for (int i = 0; i < PLAYER_BULLET_PRE_WARM_COUNT; i++)
        {
            tempBullets.Add(playerBulletPool.Get());
        }

        for (int i = 0; i < PLAYER_BULLET_PRE_WARM_COUNT; i++)
        {
            playerBulletPool.Release(tempBullets[i]);
        }
    }

    private PlayerBullet CreateBullet()
    { 
        GameObject obj = Instantiate(playerBulletPrefab);
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();
        bullet.SetPool(playerBulletPool);
        
        return bullet;
    }

    private void OnTakeFromPool(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.IsDespawned = false;
    }

    private void OnReturnedToPool(PlayerBullet bullet)
    {
        bullet.IsDespawned = true;
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(PlayerBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
