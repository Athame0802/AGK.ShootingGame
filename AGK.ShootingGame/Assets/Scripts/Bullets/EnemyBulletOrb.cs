using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletOrb : MonoBehaviour, IDespawnable, IBullet
{
    private ObjectPool<EnemyBulletOrb> pool = default;
    [SerializeField] private float bulletSpeed = default;
    [SerializeField] private Transform playerTransform = default;

    public bool IsDespawned { get; set; } = default;

    private void Awake()
    {
        PlayerAttack player = GameObject.FindAnyObjectByType<PlayerAttack>();
        playerTransform = player.transform;
    }

    private void OnEnable()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetPool(ObjectPool<EnemyBulletOrb> pool)
    {
        this.pool = pool;
    }

    private void Update()
    {
        Move(bulletSpeed);
        CheckObject(Layers.Player);
    }

    private void Move(float bulletSpeed)
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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

        damageable.TakeDamage(1);
        pool.Release(this);
    }
}
