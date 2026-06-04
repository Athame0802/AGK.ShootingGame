using UnityEngine;

[RequireComponent(typeof(EnemyDropItem))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = default;
    [SerializeField] private EnemyDropItem enemyDropItem = default;

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