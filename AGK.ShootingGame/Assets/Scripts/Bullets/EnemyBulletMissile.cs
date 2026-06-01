using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletMissile : MonoBehaviour, IDespawnable, IBullet
{
    private ObjectPool<EnemyBulletMissile> pool = default;
    [SerializeField] private float bulletSpeed = default;
    [SerializeField] private Transform playerTransform = default;

    public bool IsDespawned { get; set; } = default;
    
    private float time = default;
    private const float HOMING_TIME = 1.25f;

    private void Awake()
    {
        PlayerAttack player = GameObject.FindAnyObjectByType<PlayerAttack>();
        playerTransform = player.transform;
    }

    private void OnEnable()
    {
        time = 0f;
        RotateToPlayer();
    }
    
    private void Update()
    {
        time += Time.deltaTime;

        if (time <= HOMING_TIME)
        {
            RotateToPlayer();
        }

        Move(bulletSpeed);
        CheckObject(Layers.Player);
    }

    public void SetPool(ObjectPool<EnemyBulletMissile> pool)
    {
        this.pool = pool;
    }


    private void Move(float bulletSpeed)
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
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

    private void RotateToPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
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
