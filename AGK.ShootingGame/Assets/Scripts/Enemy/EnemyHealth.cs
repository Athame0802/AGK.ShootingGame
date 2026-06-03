using UnityEngine;

[RequireComponent(typeof(EnemyDropItem))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = default;
    [SerializeField] private EnemyDropItem enemyDropItem = default;
    public bool IsEnabled { get { return this.enabled; } } // 리펙토링 시급

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enemyDropItem.Drop(transform);
        Destroy(gameObject);
    }
}