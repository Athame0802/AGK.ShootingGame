using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBullet : MonoBehaviour, IDespawnable
{
    private ObjectPool<PlayerBullet> pool = default;
    [SerializeField] private float bulletSpeed = default;

    public bool IsDespawned { get; set; } = default;

    public void SetPool(ObjectPool<PlayerBullet> pool)
    {
        this.pool = pool;
    }

    private void Update()
    {
        Move(bulletSpeed);
    }

    private void Move(float bulletSpeed)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpeed * Time.deltaTime, 0);
    }

    public void Despawn()
    {
        if (IsDespawned)
        {
            return;
        }

        pool.Release(this);
    }
}