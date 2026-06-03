using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Bullet : MonoBehaviour, IDespawnable
{
    protected IBulletPool bulletPool { get; set; } = default;
    protected Action<Bullet> returnToPoolAction = default;

    [SerializeField] protected float bulletSpeed = default;
    [SerializeField] protected int damage = 1;
    
    public static Transform playerTransform { get; private set; } = default;

    public bool IsDespawned { get; set; } = default;

    public virtual void Construct(IBulletPool bulletPool, Action<Bullet> returnAction, Transform pTransform)
    {
        this.bulletPool = bulletPool;
        returnToPoolAction += returnAction;
        
        if (playerTransform == null)
            playerTransform = pTransform;
    }

    protected void Move(float bulletSpeed)
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    public void Despawn()
    {
        if (IsDespawned)
        {
            return;
        }

        returnToPoolAction?.Invoke(this);
    }

    protected void RotateToPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angleRight = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleRight - 90);
    }

    protected void CheckLayerAndDamage(int layer, int damage = 1)
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

        damageable.TakeDamage(damage);
        Despawn();
    }

    public abstract void SpawnAtLocation(Transform transform);

    public abstract void MakeMyTypeBulletPool(IBulletPool bulletPool);
}