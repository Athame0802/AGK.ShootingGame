using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBullet : MonoBehaviour, IDespawnable, IBullet
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
        CheckObject(Layers.Enemy);
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

    public void CheckObject(int layer)
    {
        Collider2D collider = Physics2D.OverlapBox(
            transform.position,
            new Vector2(transform.localScale.x, transform.localScale.y),
            0f,
            1 << layer
            );

        if (collider == null)
        {
            return;
        }

        if (!collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            return;
        }

        if (damageable.IsEnabled)
        { 
            damageable.TakeDamage(1);
            pool.Release(this);
        }
    }
}